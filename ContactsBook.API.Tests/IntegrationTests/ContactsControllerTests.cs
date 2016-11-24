using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using ContactsBook.API.Controllers;
using ContactsBook.API.Tests.Base;
using ContactsBook.Framework.Contracts.Services;
using ContactsBook.Framework.Dtos;
using ContactsBook.Framework.Entities;
using ContactsBook.Framework.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace ContactsBook.API.Tests.IntegrationTests
{
    [TestClass]
    public class ContactsControllerTests: BaseControllersTest
    {
        #region Private methods

        private ContactsController GetController()
        {
            var controller = new ContactsController(this.Factory);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            return controller;
        }

        private static Contact GetContact(int id)
        {
            return new Contact()
            {
                ContactId = id,
                FirstName = "fn" + id,
                LastName = "ln" + id,
                Emails = new List<string> {"email" + id + "@gmail.com"}
            };
        }

        #endregion


        #region Get methods

        [TestMethod]
        public void get_list_return_non_null_list()
        {
            var mockService = MockRepository.GenerateMock<IContactsService>();
            mockService.Expect(x => x.GetFiltered(Arg.Is(string.Empty))).Return(new List<ContactInfoDto>());
            this.Factory.ContactsService = mockService;

            var response = GetController().Get();

            Assert.IsTrue(response != null);
        }

        [TestMethod]
        public void get_invalid_contact_returns_404()
        {
            const int contactId = 1;
            var mockService = MockRepository.GenerateMock<IContactsService>();
            mockService.Expect(x => x.GetById(Arg.Is(contactId))).Return(null);
            this.Factory.ContactsService = mockService;

            var response = GetController().Get(contactId);

            Assert.IsInstanceOfType(response, typeof (NotFoundResult));
        }

        [TestMethod]
        public void get_valid_contact_returns_valid_entity()
        {
            var contact = GetContact(1);
            var mockService = MockRepository.GenerateMock<IContactsService>();
            mockService.Expect(x => x.GetById(Arg.Is(contact.ContactId))).Return(contact);
            this.Factory.ContactsService = mockService;

            var response = GetController().Get(contact.ContactId);

            Assert.IsInstanceOfType(response, typeof (OkNegotiatedContentResult<Contact>));
            var result = response as OkNegotiatedContentResult<Contact>;
            Assert.IsTrue(result?.Content != null);
            Assert.AreEqual(result.Content, contact);
        }

        #endregion


        #region Delete methods

        [TestMethod]
        public void remove_invalid_contact_returns_bad_request()
        {
            const int contactId = 1;
            var mockService = MockRepository.GenerateMock<IContactsService>();
            mockService.Expect(x => x.Remove(Arg.Is(contactId))).Throw(new BusinessException("Invalid id"));
            this.Factory.ContactsService = mockService;

            var response = GetController().Delete(contactId);

            Assert.IsInstanceOfType(response, typeof (BadRequestErrorMessageResult));
        }

        [TestMethod]
        public void remove_valid_contact_returns_ok()
        {
            const int contactId = 1;
            var mockService = MockRepository.GenerateMock<IContactsService>();
            mockService.Expect(x => x.Remove(Arg.Is(contactId)));
            this.Factory.ContactsService = mockService;

            var response = GetController().Delete(contactId);

            Assert.IsInstanceOfType(response, typeof (OkResult));
        }

        #endregion


        #region Post methods

        [TestMethod]
        public void add_invalid_contact_returns_bad_request()
        {
            var contact = GetContact(1);
            var mockService = MockRepository.GenerateMock<IContactsService>();
            mockService.Expect(x => x.Add(Arg.Is(contact))).Throw(new BusinessException("Invalid contact"));
            this.Factory.ContactsService = mockService;

            var response = GetController().Post(contact);

            Assert.IsInstanceOfType(response, typeof (BadRequestErrorMessageResult));
        }

        [TestMethod]
        public void add_valid_contact_returns_ok()
        {
            var contact = GetContact(1);
            var mockService = MockRepository.GenerateMock<IContactsService>();
            mockService.Expect(x => x.Add(Arg.Is(contact)));
            this.Factory.ContactsService = mockService;

            var response = GetController().Post(contact);

            Assert.IsInstanceOfType(response, typeof (OkResult));
        }

        #endregion


        #region Put methods

        [TestMethod]
        public void update_invalid_contact_returns_bad_request()
        {
            var contact = GetContact(1);
            var mockService = MockRepository.GenerateMock<IContactsService>();
            mockService.Expect(x => x.Update(Arg.Is(contact))).Throw(new BusinessException("Invalid contact"));
            this.Factory.ContactsService = mockService;

            var response = GetController().Put(contact);

            Assert.IsInstanceOfType(response, typeof (BadRequestErrorMessageResult));
        }

        [TestMethod]
        public void update_valid_contact_returns_ok()
        {
            var contact = GetContact(1);
            var mockService = MockRepository.GenerateMock<IContactsService>();
            mockService.Expect(x => x.Update(Arg.Is(contact)));
            this.Factory.ContactsService = mockService;

            var response = GetController().Put(contact);

            Assert.IsInstanceOfType(response, typeof (OkResult));
        }

        #endregion

    }
}
