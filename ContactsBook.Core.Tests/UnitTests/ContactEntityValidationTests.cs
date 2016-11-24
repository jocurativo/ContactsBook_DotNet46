using System.Collections.Generic;
using System.Linq;
using ContactsBook.Core.Tests.Base;
using ContactsBook.Framework.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ContactsBook.Core.Tests.UnitTests
{
    [TestClass]
    public class ContactEntityValidationTests : BaseTest
    {
        private Contact GetContact()
        {
            return new Contact()
            {
                ContactId = 1,
                FirstName = "fn",
                LastName = "ln",
                Emails = new List<string> { "one@gmail.com", "two@gmail.com" }
            };
        }

        [TestMethod]
        public void all_fields_correct_return_no_validations()
        {
            var c = GetContact();
            Assert.IsTrue(!c.Validate().Any());
        }

        #region First name tests

        [TestMethod]
        public void first_name_cannot_be_null()
        {
            var c = GetContact();
            c.FirstName = null;
            Assert.IsTrue(c.Validate().Count() == 1);
        }

        [TestMethod]
        public void first_name_cannot_be_empty()
        {
            var c = GetContact();
            c.FirstName = string.Empty;
            Assert.IsTrue(c.Validate().Count() == 1);
        }

        [TestMethod]
        public void first_name_have_length_max_50()
        {
            var c = GetContact();
            c.FirstName = new string('A', 50);
            Assert.IsTrue(!c.Validate().Any());
        }

        [TestMethod]
        public void first_name_cannot_have_length_greater_than_50()
        {
            var c = GetContact();
            c.FirstName = new string('A', 51);
            Assert.IsTrue(c.Validate().Count() == 1);
        }

        #endregion

        #region Last name tests

        [TestMethod]
        public void last_name_cannot_be_null()
        {
            var c = GetContact();
            c.LastName = null;
            Assert.IsTrue(c.Validate().Count() == 1);
        }

        [TestMethod]
        public void last_name_cannot_be_empty()
        {
            var c = GetContact();
            c.LastName = string.Empty;
            Assert.IsTrue(c.Validate().Count() == 1);
        }

        [TestMethod]
        public void last_name_have_length_max_50()
        {
            var c = GetContact();
            c.LastName = new string('A', 50);
            Assert.IsTrue(!c.Validate().Any());
        }

        [TestMethod]
        public void last_name_cannot_have_length_greater_than_50()
        {
            var c = GetContact();
            c.LastName = new string('A', 51);
            Assert.IsTrue(c.Validate().Count() == 1);
        }
        #endregion

        #region Emails tests

        [TestMethod]
        public void emails_list_cannot_be_null()
        {
            var c = GetContact();
            c.Emails = null;
            Assert.IsTrue(c.Validate().Count() == 1);
        }

        [TestMethod]
        public void emails_list_cannot_be_empty()
        {
            var c = GetContact();
            c.Emails = new List<string>();
            Assert.IsTrue(c.Validate().Count() == 1);
        }

        [TestMethod]
        public void emails_list_cannot_contains_repeated_emails()
        {
            var c = GetContact();
            c.Emails = new List<string> { "one@gmail.com", "two@gmail.com", "one@gmail.com" };
            Assert.IsTrue(c.Validate().Count() == 1);
        }

        [TestMethod]
        public void emails_list_cannot_contains_invalid_emails()
        {
            var c = GetContact();
            c.Emails = new List<string> { "one@gmail.com", "two_gmail.com" };
            Assert.IsTrue(c.Validate().Count() == 1);
        }

        [TestMethod]
        public void emails_list_cannot_contains_null_emails()
        {
            var c = GetContact();
            c.Emails = new List<string> { "one@gmail.com", null };
            Assert.IsTrue(c.Validate().Count() == 1);
        }

        [TestMethod]
        public void emails_list_cannot_contains_empty_emails()
        {
            var c = GetContact();
            c.Emails = new List<string> { "one@gmail.com", string.Empty };
            Assert.IsTrue(c.Validate().Count() == 1);
        }
        #endregion
    }
}
