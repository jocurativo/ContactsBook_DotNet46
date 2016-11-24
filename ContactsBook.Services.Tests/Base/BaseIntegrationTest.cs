using ContactsBook.Framework.Contracts.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace ContactsBook.Services.Tests.Base
{
    public abstract class BaseIntegrationTest
    {
        protected FakeUnitOfWork TestUnitOfWork { get; private set; }
        protected ServiceFactory ServiceFactory { get; private set; }

        public BaseIntegrationTest()
        {
            TestUnitOfWork = new FakeUnitOfWork();
            ServiceFactory = new ServiceFactory(TestUnitOfWork);
        }

        [TestInitialize]
        public void Initialize()
        {
        }

        [TestCleanup]
        public void CleanUp()
        {
            TestUnitOfWork.ContactsRepository = null;
        }
    }
}
