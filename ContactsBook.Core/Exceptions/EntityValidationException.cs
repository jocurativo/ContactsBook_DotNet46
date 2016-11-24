using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsBook.Framework.Exceptions
{
    public class EntityValidationException: Exception
    {
        public IEnumerable<ValidationResult> Validations { get; private set; }
        public EntityValidationException(IEnumerable<ValidationResult> validations)
        {
            Validations = validations;
        }
    }
}
