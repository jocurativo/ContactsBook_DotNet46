using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using ContactsBook.Framework.Contracts.Factories;

namespace ContactsBook.API
{
    public abstract class BaseController: ApiController
    {
        protected IServiceFactory Factory { get; private set; }
        public BaseController(IServiceFactory factory)
        {
            Factory = factory;
        }
    }
}