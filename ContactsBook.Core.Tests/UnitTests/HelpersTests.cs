using ContactsBook.Core.Tests.Base;
using ContactsBook.Framework.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ContactsBook.Core.Tests.UnitTests
{
    [TestClass]
    public class HelpersTests: BaseTest
    {
        [TestMethod]
        public void null_email_returns_false()
        {
            Assert.IsFalse(ValidationsHelper.IsValidEmail(null));
        }
        [TestMethod]
        public void empty_email_returns_false()
        {
            Assert.IsFalse(ValidationsHelper.IsValidEmail(string.Empty));
        }
        [TestMethod]
        public void invalid_email_returns_false()
        {
            Assert.IsFalse(ValidationsHelper.IsValidEmail("  "));
            Assert.IsFalse(ValidationsHelper.IsValidEmail("abc1223.com"));
            Assert.IsFalse(ValidationsHelper.IsValidEmail("abc@world"));
            Assert.IsFalse(ValidationsHelper.IsValidEmail("abc@world@com"));
        }
        [TestMethod]
        public void valid_email_returns_true()
        {
            Assert.IsTrue(ValidationsHelper.IsValidEmail("test@gmail.com"));
            Assert.IsTrue(ValidationsHelper.IsValidEmail("test.123.abc@gmail.com"));
            Assert.IsTrue(ValidationsHelper.IsValidEmail("test_123_abc@gmail.com"));
        }
    }
}
