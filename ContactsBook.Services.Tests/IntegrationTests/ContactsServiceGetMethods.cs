using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactsBook.Framework.Contracts.Repositories;
using ContactsBook.Framework.Dtos;
using ContactsBook.Framework.Entities;
using ContactsBook.Framework.Exceptions;
using ContactsBook.Services.Tests.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace ContactsBook.Services.Tests.IntegrationTests
{
    [TestClass]
    public class ContactsServiceGetMethods: BaseIntegrationTest
    {
        private Contact GetContact(int id)
        {
            return new Contact()
            {
                ContactId = id,
                FirstName = "fn" + id,
                LastName = "ln" + id,
                Emails = new List<string> { "email" + id + "@gmail.com" }
            };
        }

        private ContactInfoDto GetContactDto(int id)
        {
            return new ContactInfoDto()
            {
                ContactId = id,
                FirstName = "fn" + id,
                LastName = "ln" + id
            };
        }

        [TestMethod]
        public void get_invalid_contact_returns_null()
        {
            const int contactId = 1;
            var repoMock = MockRepository.GenerateMock<IContactsRepository>();

            repoMock.Expect(x => x.GetContact(Arg.Is<int>(contactId))).Return(null);

            TestUnitOfWork.ContactsRepository = repoMock;

            var contact = ServiceFactory.ContactsService.GetById(contactId);

            Assert.IsNull(contact);
        }

        [TestMethod]
        public void get_valid_contact_returns_the_object()
        {
            var contact = GetContact(1);
            var repoMock = MockRepository.GenerateMock<IContactsRepository>();

            repoMock.Expect(x => x.GetContact(Arg.Is<int>(contact.ContactId))).Return(contact);

            TestUnitOfWork.ContactsRepository = repoMock;

            var result = ServiceFactory.ContactsService.GetById(contact.ContactId);

            Assert.AreEqual(contact, result);
        }

        [TestMethod]
        public void get_filtered_returns_a_collection()
        {
            var contacts = new List<ContactInfoDto> { GetContactDto(1), GetContactDto(2) };
            var repoMock = MockRepository.GenerateMock<IContactsRepository>();
            const string filter = "Some Filter";

            repoMock.Expect(x => x.GetList(Arg.Is(filter))).Return(contacts);

            TestUnitOfWork.ContactsRepository = repoMock;

            var result = ServiceFactory.ContactsService.GetFiltered(filter);

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(contacts[0], result.ElementAt(0));
            Assert.AreEqual(contacts[1], result.ElementAt(1));
        }

        [TestMethod]
        public void get_filtered_never_returns_null_collection()
        {
            var repoMock = MockRepository.GenerateMock<IContactsRepository>();
            const string filter = "Some Filter";

            repoMock.Expect(x => x.GetList(Arg.Is(filter))).Return(new List<ContactInfoDto>());

            TestUnitOfWork.ContactsRepository = repoMock;

            var result = ServiceFactory.ContactsService.GetFiltered(filter);

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException))]
        public void get_contact_using_an_invalid_email_throws_exception()
        {
            var repoMock = MockRepository.GenerateMock<IContactsRepository>();

            TestUnitOfWork.ContactsRepository = repoMock;

            ServiceFactory.ContactsService.GetByEmail("not_an_email");
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException))]
        public void get_contact_using_a_null_email_throws_exception()
        {
            var repoMock = MockRepository.GenerateMock<IContactsRepository>();

            TestUnitOfWork.ContactsRepository = repoMock;

            ServiceFactory.ContactsService.GetByEmail(null);
        }

        [TestMethod]
        public void get_contact_using_a_valid_email_return_contact()
        {
            var contact = GetContact(1);
            var repoMock = MockRepository.GenerateMock<IContactsRepository>();

            repoMock.Expect(x => x.GetContact(Arg.Is<string>(contact.Emails.First()))).Return(contact);

            TestUnitOfWork.ContactsRepository = repoMock;

            var result = ServiceFactory.ContactsService.GetByEmail(contact.Emails.First());

            Assert.IsNotNull(result);
            Assert.AreEqual(contact, result);
        }
    }
}
