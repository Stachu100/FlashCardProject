using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIFlashCard.Models
{
    public class EncryptionKeys
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID_encryptionKeys { get; set; }

        public int ID_User { get; set; }

        [Required]
        public byte[] EncryptionKey { get; set; }

        [Required]
        public byte[] IV { get; set; }
    }
}