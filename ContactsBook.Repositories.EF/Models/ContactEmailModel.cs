using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactsBook.Repositories.EF.Models
{
    [Table("contact_email")]
    public class ContactEmailModel
    {
        [Key]
        [MaxLength(100)]
        [Required]
        [Column("email", TypeName = "varchar")]
        public string Email { get; set; }

        [Column("contact_id")]
        public int ContactId { get; set; }
    }
}