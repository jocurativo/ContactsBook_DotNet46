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
    public class ContactsServiceUpdateTests: BaseIntegrationTest
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

        [TestMethod]
        [ExpectedException(typeof(InvalidParametersException))]
        public void update_null_contact_throws_exception()
        {
            TestUnitOfWork.ContactsRepository = MockRepository.GenerateMock<IContactsRepository>();
            ServiceFactory.ContactsService.Update(null);
        }

        [TestMethod]
        [ExpectedException(typeof(EntityValidationException))]
        public void update_invalid_contact_throws_exception()
        {
            var contact = GetContact(1);
            contact.Emails = new List<string>();
            TestUnitOfWork.ContactsRepository = MockRepository.GenerateMock<IContactsRepository>();

            ServiceFactory.ContactsService.Update(contact);
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException))]
        public void update_contact_with_email_in_use_by_another_contact_throws_exception()
        {
            var contact = GetContact(1);
            var repoMock = MockRepository.GenerateMock<IContactsRepository>();

            foreach (var email in contact.Emails)
                repoMock.Expect(x => x.GetContactIdForEmail(Arg.Is(email))).Return(1);

            TestUnitOfWork.ContactsRepository = repoMock;

            ServiceFactory.ContactsService.Update(contact);
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException))]
        public void update_contact_with_invalid_contact_throws_exception()
        {
            var contact = GetContact(1);
            var repoMock = MockRepository.GenerateMock<IContactsRepository>();

            foreach (var email in contact.Emails)
                repoMock.Expect(x => x.GetContactIdForEmail(Arg.Is(email))).Return(null);

            repoMock.Expect(x => x.GetContactInfo(Arg.Is(contact.ContactId))).Return(null);

            TestUnitOfWork.ContactsRepository = repoMock;

            ServiceFactory.ContactsService.Update(contact);
        }

        [TestMethod]
        public void update_contact_calls_update_method()
        {
            var contact = GetContact(1);
            var repoMock = MockRepository.GenerateMock<IContactsRepository>();

            foreach (var email in contact.Emails)
                repoMock.Expect(x => x.GetContactIdForEmail(Arg.Is(email))).Return(null);

            repoMock.Expect(x => x.GetContactInfo(Arg.Is(contact.ContactId))).Return(new ContactInfoDto());
            repoMock.Expect(x => x.Update(Arg.Is(contact)));
            TestUnitOfWork.ContactsRepository = repoMock;

            ServiceFactory.ContactsService.Update(contact);
        }
    }
}
