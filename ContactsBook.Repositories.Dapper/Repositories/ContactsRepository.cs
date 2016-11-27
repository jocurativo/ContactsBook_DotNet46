using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactsBook.Framework.Contracts.Repositories;
using ContactsBook.Framework.Dtos;
using ContactsBook.Framework.Entities;
using Dapper;

namespace ContactsBook.Repositories.Dapper.Repositories
{
    public class ContactsRepository: BaseRepository, IContactsRepository
    {
        public ContactsRepository(IDbConnection connection, Func<IDbTransaction> transaction) 
            : base(connection, transaction)
        {}

        #region IContactsRepository

        public void Add(Contact contact)
        {
            const string queryContact = "insert into contact(contact_first_name, contact_last_name) values (@fn, @ln);"+
                                        "SELECT CAST(SCOPE_IDENTITY() as int)";
            const string queryEmails = "insert into contact_email(contact_id, email) values (@id, @email)";

            var id = Connection.Query<int>(queryContact, new {fn = contact.FirstName, ln = contact.LastName}, this.Transaction);

            Connection.Execute(queryEmails, contact.Emails.Select(x => new {id, email = x}), this.Transaction);
        }

        public void Update(Contact contact)
        {
            const string queryUpdateContact = "update contact set contact_first_name = @fn, contact_last_name = @ln where contact_id = @id";
            const string queryDeleteEmails = "delete from contact_email where contact_id = @id";
            const string queryAddEmails = "insert into contact_email(contact_id, email) values (@id, @email)";

            Connection.Execute(queryUpdateContact, new { id = contact.ContactId, fn = contact.FirstName, ln = contact.LastName }, this.Transaction);
            Connection.Execute(queryDeleteEmails, new { id = contact .ContactId }, this.Transaction);
            Connection.Execute(queryAddEmails, contact.Emails.Select(x => new { id = contact.ContactId, email = x }), this.Transaction);
        }

        public bool Remove(int id)
        {
            const string query = "delete from contact where contact_id = @value";

            return Connection.Execute(query, new { value = id }, this.Transaction) > 0;
        }

        public Contact GetContact(int id)
        {
            const string queryEmails = "select email from contact_email where contact_id = @value";

            var contactInfo = GetContactInfo(id);

            if (contactInfo == null)
                return null;

            return new Contact()
            {
                ContactId = contactInfo.ContactId,
                FirstName = contactInfo.FirstName,
                LastName = contactInfo.LastName,
                Emails = Connection.Query<string>(queryEmails, new { value = contactInfo.ContactId }).ToList()
            };
        }

        public Contact GetContact(string email)
        {
            var id = GetContactIdForEmail(email);

            return !id.HasValue
                ? null
                : GetContact(id.Value);
        }

        public ContactInfoDto GetContactInfo(int id)
        {
            const string query = "select " +
                                 "  contact_id as ContactId, " +
                                 "  contact_first_name as FirstName, " +
                                 "  contact_last_name as LastName " +
                                 " from contact " +
                                 " where contact_id = @value ";

            var parameters = new { value = id };

            return Connection
                .Query<ContactInfoDto>(query, parameters)
                .SingleOrDefault();
        }

        public IEnumerable<ContactInfoDto> GetList(string pattern = null)
        {
            const string  query = "select distinct " +
                        "  contact.contact_id as ContactId, " +
                        "  contact.contact_first_name as FirstName, " +
                        "  contact.contact_last_name as LastName " +
                        " from contact " +
                        "  inner join contact_email on contact_email.contact_id = contact.contact_id " +
                        " where contact_first_name like @value " +
                        "  or contact_last_name like @value " +
                        "  or contact_email.email like @value ";

            var toSearch = string.IsNullOrWhiteSpace(pattern) ? string.Empty : pattern.Trim();
            var parameters = new { value = "%" + toSearch + "%"};

            return Connection
                .Query<ContactInfoDto>(query, parameters)
                .ToList();
        }

        public int? GetContactIdForEmail(string email)
        {
            const string query = "select contact_id as ContactId from contact_email where lower(email) = lower(@value)";
            var parameters = new { value = email };

            var contact = Connection
                .Query<ContactInfoDto>(query, parameters)
                .SingleOrDefault();

            return contact == null ? null : (int?) contact.ContactId;
        }

        #endregion
    }
}
