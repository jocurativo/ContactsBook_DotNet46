using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactsBook.Framework.Contracts;

namespace ContactsBook.Framework.Configuration
{
    public static class ApplicationConfiguration
    {
        public static IApplicationResolver Resolver { get; set; }
        static ApplicationConfiguration()
        {
        }
    }
}
