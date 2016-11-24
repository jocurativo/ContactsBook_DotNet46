using ContactsBook.Framework.Contracts.Factories;

namespace ContactsBook.Services
{
    public abstract class BaseService
    {
        protected IUnitOfWork UnitOfWork { get; private set; }

        protected BaseService(IUnitOfWork uow)
        {
            UnitOfWork = uow;
        }
    }
}
