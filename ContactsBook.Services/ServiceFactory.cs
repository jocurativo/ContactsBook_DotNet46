using ContactsBook.Framework.Contracts.Factories;
using ContactsBook.Framework.Contracts.Services;

namespace ContactsBook.Services
{
    public class ServiceFactory: IServiceFactory
    {
        private ContactsService _contactsService = null;
        private readonly IUnitOfWork _factory = null;
        public ServiceFactory(IUnitOfWork factory)
        {
            _factory = factory;
        }
        public IContactsService ContactsService => _contactsService ?? (_contactsService = new ContactsService(_factory));
    }
}
