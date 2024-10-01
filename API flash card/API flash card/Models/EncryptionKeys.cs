using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_flash_card.Models
{
    public class EncryptionKeys
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int ID_User { get; set; }

        [Required]
        public byte[] EncryptionKey { get; set; }

        [Required]
        public byte[] IV { get; set; }

        [ForeignKey("ID_User")]
        public User User { get; set; }
    }
}
