using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIFlashCard.Models
{
    public class Countries
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID_Country { get; set; }

        [Required]
        [MaxLength(255)]
        public string Country { get; set; }

        [MaxLength(255)]
        public string Url { get; set; }
    }
}