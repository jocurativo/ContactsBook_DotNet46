using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactsBook.Framework.Dtos;
using ContactsBook.Framework.Entities;

namespace ContactsBook.Framework.Contracts.Repositories
{
    public interface IContactsRepository
    {
        void Add(Contact contact);
        void Update(Contact contact);
        bool Remove(int id);
        Contact GetContact(int id);
        Contact GetContact(string email);
        ContactInfoDto GetContactInfo(int id);
        IEnumerable<ContactInfoDto> GetList(string pattern = null);
        int? GetContactIdForEmail(string email);
    }
}
