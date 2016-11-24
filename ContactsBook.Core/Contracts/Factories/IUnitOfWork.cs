using System;
using ContactsBook.Framework.Contracts.Repositories;

namespace ContactsBook.Framework.Contracts.Factories
{
    public interface IUnitOfWork: IDisposable
    {
        IContactsRepository ContactsRepository { get; }
        void BeginTransaction();
        void CommitChanges();
        void RollbackChanges();
    }
}
