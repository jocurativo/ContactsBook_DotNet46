using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ContactsBook.Framework.Contracts.Repositories;
using ContactsBook.Framework.Dtos;
using ContactsBook.Framework.Entities;
using ContactsBook.Repositories.EF.Contexts;
using ContactsBook.Repositories.EF.Models;

namespace ContactsBook.Repositories.EF.Repositories
{
    //todo: commit changes on the bll
    public class ContactsRepository: BaseRepository, IContactsRepository
    {
        public ContactsRepository(ContactsBookContext context)
            :base(context)
        {}

        #region IContactsRepository

        public void Add(Contact contact)
        {
            Context.Contacts.Add(new ContactModel()
            {
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                Emails = contact
                    .Emails
                    .Select(x => new ContactEmailModel()
                    {
                        Email = x.ToLower()
                    })
                    .ToList()
            });
        }

        public void Update(Contact contact)
        {
            var toUpdate = Context.Contacts.SingleOrDefault(x => x.ContactId == contact.ContactId);

            if (toUpdate == null)
                return;

            toUpdate.FirstName = contact.FirstName;
            toUpdate.LastName = contact.LastName;

            Context.ContactEmails.RemoveRange(toUpdate.Emails.Where(x => !contact.Emails.Contains(x.Email)));

            Context.ContactEmails.AddRange(contact.Emails
                .Where(x => toUpdate.Emails.All(y => y.Email != x))
                .Select(x => new ContactEmailModel()
                {
                    Email = x,
                    ContactId = toUpdate.ContactId
                }));
        }

        public bool Remove(int id)
        {
            var toDelete = Context.Contacts.SingleOrDefault(x => x.ContactId == id);

            if (toDelete == null)
                return false;

            Context.Contacts.Remove(toDelete);
            return true;
        }

        public Contact GetContact(int id)
        {
            var contact = Context.Contacts.SingleOrDefault(x => x.ContactId == id);

            return contact == null
                ? null
                : MapToEntity(contact);
        }

        public Contact GetContact(string email)
        {
            var contact =
                Context.Contacts.FirstOrDefault(
                    x => x.Emails != null && x.Emails.Any(y => y.Email.ToLower() == email.ToLower()));

            return contact == null
                ? null
                : MapToEntity(contact);
        }

        public ContactInfoDto GetContactInfo(int id)
        {
            return Context
                .Contacts
                .Select(MapToDto)
                .SingleOrDefault(x => x.ContactId == id);
        }

        public IEnumerable<ContactInfoDto> GetList(string pattern = null)
        {
            var query = Context.Contacts.AsQueryable();

            if (!string.IsNullOrEmpty(pattern))
                query = query.Where(x => x.FirstName.Contains(pattern) ||
                                         x.LastName.Contains(x.LastName) ||
                                         (x.Emails != null && x.Emails.Any(y => y.Email.Contains(pattern)))
                    );

            return query
                .OrderBy(x => x.FirstName)
                .ThenBy(x => x.LastName)
                .Select(MapToDto).
                ToList();
        }

        public int? GetContactIdForEmail(string email)
        {
            return Context
                .ContactEmails
                .SingleOrDefault(x => x.Email.ToLower() == email.ToLower())?.ContactId;
        }

        #endregion


        #region Private methods

        private static Contact MapToEntity(ContactModel model)
        {
            return new Contact()
            {
                ContactId = model.ContactId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Emails = model.Emails.Select(y => y.Email)
            };
        }

        private static ContactInfoDto MapToDto(ContactModel model)
        {
            return new ContactInfoDto()
            {
                ContactId = model.ContactId,
                FirstName = model.FirstName,
                LastName = model.LastName
            };
        }

        #endregion

    }
}
