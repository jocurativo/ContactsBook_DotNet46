using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactsBook.Framework.Contracts.Repositories;
using ContactsBook.Framework.Entities;
using ContactsBook.Framework.Exceptions;
using ContactsBook.Services.Tests.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace ContactsBook.Services.Tests.IntegrationTests
{
    [TestClass]
    public class ContactsServiceAddTests: BaseIntegrationTest
    {
        private Contact GetContact()
        {
            return new Contact()
            {
                ContactId = 0,
                FirstName = "fn0",
                LastName = "ln0",
                Emails = new List<string> { "email0@gmail.com"}
            };
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidParametersException))]
        public void add_null_contact_throws_exception()
        {
            TestUnitOfWork.ContactsRepository = MockRepository.GenerateMock<IContactsRepository>();
            ServiceFactory.ContactsService.Add(null);
        }

        [TestMethod]
        [ExpectedException(typeof(EntityValidationException))]
        public void add_invalid_contact_throws_exception()
        {
            var contact = GetContact();
            contact.Emails = new List<string>();
            TestUnitOfWork.ContactsRepository = MockRepository.GenerateMock<IContactsRepository>();

            ServiceFactory.ContactsService.Add(contact);
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException))]
        public void add_contact_with_email_in_use_by_another_contact_throws_exception()
        {
            var contact = GetContact();
            var repoMock = MockRepository.GenerateMock<IContactsRepository>();
            repoMock.Expect(x => x.GetContactIdForEmail(Arg.Is(contact.Emails.First()))).Return(1);
            
            TestUnitOfWork.ContactsRepository = repoMock;

            ServiceFactory.ContactsService.Add(contact);
        }

        [TestMethod]
        public void add_contact_calls_add_method()
        {
            var contact = GetContact();
            var repoMock = MockRepository.GenerateMock<IContactsRepository>();

            foreach (var email in contact.Emails)
                repoMock.Expect(x => x.GetContactIdForEmail(Arg.Is(email))).Return(null);

            repoMock.Expect(x => x.Add(Arg.Is<Contact>(contact)));
            TestUnitOfWork.ContactsRepository = repoMock;

            ServiceFactory.ContactsService.Add(contact);
        }
    }
}
