using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIFlashCard.Models
{
    public class EncryptionKeys
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID_encryptionKeys { get; set; }

        [Required]
        public int ID_User { get; set; }

        [Required]
        [MinLength(32)]
        [MaxLength(32)]
        public byte[] EncryptionKey { get; set; }

        [Required]
        [MinLength(16)]
        [MaxLength(16)]
        public byte[] IV { get; set; }
    }
}