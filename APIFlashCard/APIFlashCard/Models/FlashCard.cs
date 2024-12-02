using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIFlashCard.Models
{
    public class FlashCard
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID_flashcard { get; set; }

        [Required]
        public int ID_Category { get; set; }

        [Required]
        [MaxLength(255)]
        public string FrontFlashCard { get; set; }

        [Required]
        [MaxLength(255)]
        public string BackFlashCard { get; set; }
    }
}