using System.Collections.Generic;
using System.Linq;
using ContactsBook.Framework.Contracts.Factories;
using ContactsBook.Framework.Contracts.Services;
using ContactsBook.Framework.Dtos;
using ContactsBook.Framework.Entities;
using ContactsBook.Framework.Exceptions;
using ContactsBook.Framework.Helpers;

namespace ContactsBook.Services
{
    public class ContactsService: BaseService, IContactsService
    {
        public ContactsService(IUnitOfWork uow)
            :base(uow)
        {}
        public void Add(Contact contact)
        {
            if (contact == null)
                throw new InvalidParametersException();

            var validations = contact.Validate().ToList();

            if (validations.Any())
                throw new EntityValidationException(validations);

            var emailInUse = contact.Emails.FirstOrDefault(x => UnitOfWork.ContactsRepository.GetContactIdForEmail(x).HasValue);

            if (!string.IsNullOrEmpty(emailInUse))
                throw new BusinessException(string.Format(Translations.Messages.EmailAlreadyInUse, emailInUse));

            contact.ContactId = 0;

            UnitOfWork.BeginTransaction();
            UnitOfWork.ContactsRepository.Add(contact);
            UnitOfWork.CommitChanges();
        }

        public void Update(Contact contact)
        {
            if (contact == null || contact.ContactId <= 0)
                throw new InvalidParametersException();

            var validations = contact.Validate().ToList();

            if (validations.Any())
                throw new EntityValidationException(validations);

            if (UnitOfWork.ContactsRepository.GetContactInfo(contact.ContactId) == null)
                throw new BusinessException(Translations.Messages.InvalidContactId);

            var emailInUse = contact.Emails
                .FirstOrDefault(x => UnitOfWork.ContactsRepository.GetContactIdForEmail(x).GetValueOrDefault(contact.ContactId) != contact.ContactId);

            if (!string.IsNullOrEmpty(emailInUse))
                throw new BusinessException(string.Format(Translations.Messages.InvalidEmail, emailInUse));

            UnitOfWork.BeginTransaction();
            UnitOfWork.ContactsRepository.Update(contact);
            UnitOfWork.CommitChanges();
        }

        public void Remove(int id)
        {
            UnitOfWork.BeginTransaction();
            var removed = UnitOfWork.ContactsRepository.Remove(id);

            if (!removed)
                throw new BusinessException(Translations.Messages.InvalidContactId);

            UnitOfWork.CommitChanges();
        }

        public Contact GetById(int id)
        {
            return UnitOfWork.ContactsRepository.GetContact(id);
        }

        public Contact GetByEmail(string email)
        {
            if (!ValidationsHelper.IsValidEmail(email))
                throw new BusinessException(Translations.Messages.InvalidEmail);

            return  UnitOfWork.ContactsRepository.GetContact(email);
        }

        public IEnumerable<ContactInfoDto> GetFiltered(string pattern = null)
        {
            return UnitOfWork.ContactsRepository.GetList(pattern);
        }
    }
}
