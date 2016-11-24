using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using ContactsBook.API.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ContactsBook.API.Tests.Base
{
    public abstract class BaseControllersTest
    {
        protected FakeServiceFactory Factory { get; private set; }
        public BaseControllersTest()
        {
            Factory = new FakeServiceFactory();
        }

        [TestInitialize]
        public void Initialize()
        {
        }

        [TestCleanup]
        public void CleanUp()
        {
            Factory.ContactsService = null;
        }
    }
}
