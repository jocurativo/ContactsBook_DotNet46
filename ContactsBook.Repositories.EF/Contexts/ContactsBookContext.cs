using System.Data.Entity;
using ContactsBook.Repositories.EF.Models;

namespace ContactsBook.Repositories.EF.Contexts
{
    public class ContactsBookContext: DbContext
    {
        static ContactsBookContext()
        {
            Database.SetInitializer<ContactsBookContext>(null);
        }

        public ContactsBookContext()
            : base("Name=ContactsBookContext")
        {
            Database.CommandTimeout = 300;
        }

        public DbSet<ContactModel> Contacts { get; set; }
        public DbSet<ContactEmailModel> ContactEmails { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
