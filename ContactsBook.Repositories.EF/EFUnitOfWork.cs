using System;
using System.Data.Entity;
using System.Linq;
using ContactsBook.Framework.Contracts.Factories;
using ContactsBook.Framework.Contracts.Repositories;
using ContactsBook.Repositories.EF.Contexts;
using ContactsBook.Repositories.EF.Repositories;

namespace ContactsBook.Repositories.EF
{
    public class EFUnitOfWork: IUnitOfWork
    {
        private ContactsBookContext _context = null;
        private ContactsRepository _contactsRepository = null;
        public EFUnitOfWork()
        {
            _context = new ContactsBookContext();
        }
        public IContactsRepository ContactsRepository => _contactsRepository ?? (_contactsRepository = new ContactsRepository(_context));

        public void BeginTransaction()
        {
            //We don't need it on EF
        }

        public void CommitChanges()
        {
            if (!_context.ChangeTracker.HasChanges())
                return;

            try
            {
                _context.SaveChanges();
            }
            catch (Exception)
            {
                RollbackChanges();
                throw;
            }
        }

        public void RollbackChanges()
        {
            var changedEntries = _context.ChangeTracker.Entries()
                .Where(x => x.State != EntityState.Unchanged)
                .ToList();

            foreach (var entry in changedEntries)
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.CurrentValues.SetValues(entry.OriginalValues);
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Unchanged;
                        break;
                }
            }
        }

        public void Dispose()
        {
            if (_context == null)
                return;

            _context.Dispose();
            _context = null;
        }
    }
}
