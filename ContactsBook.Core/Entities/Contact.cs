using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using ContactsBook.Framework.Helpers;

namespace ContactsBook.Framework.Entities
{
    [Serializable]
    [DataContract]
    public class Contact: BaseEntity
    {
        [DataMember]
        public int ContactId { get; set; }

        [DataMember]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "FirstNameRequired", ErrorMessageResourceType = typeof(ContactsBook.Translations.Messages))]
        [MaxLength(50, ErrorMessageResourceName = "FirstNameLength", ErrorMessageResourceType = typeof(ContactsBook.Translations.Messages))]
        public string FirstName { get; set; }

        [DataMember]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "LastNameRequired", ErrorMessageResourceType = typeof(ContactsBook.Translations.Messages))]
        [MaxLength(50, ErrorMessageResourceName = "LastNameLength", ErrorMessageResourceType = typeof(ContactsBook.Translations.Messages))]
        public string LastName { get; set; }

        [DataMember]
        public IEnumerable<string> Emails { get; set; }

        public override IEnumerable<ValidationResult> Validate()
        {
            var validations = base.Validate().ToList();

            if (Emails == null || !Emails.Any())
                validations.Add(new ValidationResult(Translations.Messages.EmailRequired));
            else
            {
                var invalidEmails = Emails
                    .Where(x => !ValidationsHelper.IsValidEmail(x))
                    .Select(x => new ValidationResult(string.Format(Translations.Messages.InvalidEmail, x)))
                    .ToList();

                if (invalidEmails.Any())
                    validations.AddRange(invalidEmails);
                else if (Emails.Count() != Emails.Distinct().Count())
                    validations.Add(new ValidationResult(Translations.Messages.EmailMustBeUnique));
            }

            return validations;
        }

        public string FullName => $"{FirstName} {LastName}";
    }
}
