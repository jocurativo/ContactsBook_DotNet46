using ContactsBook.Framework.Contracts.Factories;
using ContactsBook.Framework.Contracts.Repositories;

namespace ContactsBook.Services.Tests.Base
{
    public class FakeUnitOfWork: IUnitOfWork
    {
        public void Dispose()
        {
        }

        public IContactsRepository ContactsRepository { get; set; }
        public void BeginTransaction()
        {
        }

        public void CommitChanges()
        {
        }

        public void RollbackChanges()
        {
        }
    }
}
