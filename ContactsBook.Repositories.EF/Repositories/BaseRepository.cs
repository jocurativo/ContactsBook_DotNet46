using ContactsBook.Repositories.EF.Contexts;

namespace ContactsBook.Repositories.EF.Repositories
{
    public abstract class BaseRepository
    {
        public BaseRepository(ContactsBookContext context)
        {
            Context = context;
        }
        protected ContactsBookContext Context { get; private set; }
    }
}
