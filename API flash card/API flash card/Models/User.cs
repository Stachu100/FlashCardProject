using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_flash_card.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID_User { get; set; }

        [Required]
        [MaxLength(255)]
        public string UserName { get; set; }

        [Required]
        public byte[] UserPassword { get; set; }

        public ICollection<Category> Categories { get; set; }
        public ICollection<UserDetails> UserDetails { get; set; }
        public ICollection<EncryptionKeys> EncryptionKeys { get; set; }
    }
}
