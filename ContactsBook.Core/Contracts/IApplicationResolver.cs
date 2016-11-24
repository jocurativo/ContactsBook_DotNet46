using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsBook.Framework.Contracts
{
    public interface IApplicationResolver
    {
        T Resolve<T>();
    }
}
