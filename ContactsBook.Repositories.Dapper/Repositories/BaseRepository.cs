using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsBook.Repositories.Dapper.Repositories
{
    public abstract class BaseRepository
    {
        private Func<IDbTransaction> _transaction = null;
        public BaseRepository(IDbConnection connection, Func<IDbTransaction> transaction)
        {
            Connection = connection;
            _transaction = transaction;
        }
        protected IDbConnection Connection { get; private set; }
        protected IDbTransaction Transaction => _transaction();
    }
}
