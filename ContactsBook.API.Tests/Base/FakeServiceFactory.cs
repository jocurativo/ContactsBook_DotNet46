using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactsBook.Framework.Contracts.Factories;
using ContactsBook.Framework.Contracts.Services;

namespace ContactsBook.API.Tests.Base
{
    public class FakeServiceFactory: IServiceFactory
    {
        public IContactsService ContactsService { get; set; }
    }
}
