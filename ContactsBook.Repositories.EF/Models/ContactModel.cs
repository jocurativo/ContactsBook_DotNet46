using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactsBook.Repositories.EF.Models
{
    [Table("contact")]
    public class ContactModel
    {
        public ContactModel()
        {
            Emails = new List<ContactEmailModel>();
        }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("contact_id")]
        public int ContactId { get; set; }

        [MaxLength(50)]
        [Required]
        [Column("contact_first_name", TypeName = "varchar")]
        public string FirstName { get; set; }

        [MaxLength(50)]
        [Required]
        [Column("contact_last_name", TypeName = "varchar")]
        public string LastName { get; set; }

        public virtual IList<ContactEmailModel> Emails { get; set; } 
    }
}
