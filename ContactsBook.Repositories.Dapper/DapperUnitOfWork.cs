using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactsBook.Framework.Contracts.Factories;
using ContactsBook.Framework.Contracts.Repositories;
using ContactsBook.Repositories.Dapper.Repositories;

namespace ContactsBook.Repositories.Dapper
{
    public class DapperUnitOfWork: IUnitOfWork
    {
        private IContactsRepository _contactsRepository = null;
        private readonly IDbConnection _connection = null;
        private IDbTransaction _transaction = null;

        public DapperUnitOfWork()
        {
            _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ContactsBookContext"].ConnectionString);
            _connection.Open();
        }
        public void Dispose()
        {
            _connection.Close();
            _connection.Dispose();
        }

        public IContactsRepository ContactsRepository => _contactsRepository ?? (_contactsRepository = new ContactsRepository(_connection, () => _transaction));
        public void BeginTransaction()
        {
            _transaction = _connection.BeginTransaction();
        }

        public void CommitChanges()
        {
            _transaction.Commit();
            _transaction = null;
        }

        public void RollbackChanges()
        {
            _transaction.Rollback();
            _transaction = null;
        }
    }
}
