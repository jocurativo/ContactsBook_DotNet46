using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactsBook.Framework.Dtos;
using ContactsBook.Framework.Entities;
using Microsoft.SqlServer.Server;

namespace ContactsBook.Framework.Contracts.Services
{
    public interface IContactsService
    {
        void Add(Contact contact);
        void Update(Contact contact);
        void Remove(int id);
        Contact GetById(int id);
        Contact GetByEmail(string email);
        IEnumerable<ContactInfoDto> GetFiltered(string pattern = null);
    }
}
