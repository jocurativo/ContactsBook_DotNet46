using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactsBook.Framework.Contracts.Repositories;
using ContactsBook.Framework.Contracts.Services;

namespace ContactsBook.Framework.Contracts.Factories
{
    public interface IServiceFactory
    {
        IContactsService ContactsService { get; }
    }
}
