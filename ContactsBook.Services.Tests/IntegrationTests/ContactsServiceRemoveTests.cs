using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactsBook.Framework.Contracts.Repositories;
using ContactsBook.Framework.Exceptions;
using ContactsBook.Services.Tests.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace ContactsBook.Services.Tests.IntegrationTests
{
    [TestClass]
    public class ContactsServiceRemoveTests: BaseIntegrationTest
    {
        [TestMethod]
        public void remove_calls_repository_method()
        {
            const int contactId = 1;
            var repoMock = MockRepository.GenerateMock<IContactsRepository>();

            repoMock.Expect(x => x.Remove(Arg.Is<int>(contactId))).Return(true);

            TestUnitOfWork.ContactsRepository = repoMock;

            ServiceFactory.ContactsService.Remove(contactId);
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException))]
        public void invalid_contact_throws_exception()
        {
            const int contactId = 1;
            var repoMock = MockRepository.GenerateMock<IContactsRepository>();

            repoMock.Expect(x => x.Remove(Arg.Is<int>(contactId))).Return(false);

            TestUnitOfWork.ContactsRepository = repoMock;

            ServiceFactory.ContactsService.Remove(contactId);
        }
    }
}
