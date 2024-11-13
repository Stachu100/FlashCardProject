using SQLite;

namespace FiszkiApp.EntityClasses.Models
{
    public class LocalFlashcardTable
    {
        public int IdFlashcard { get; set; }
        public string FrontText { get; set; }
        public string BackText { get; set; }
    }

}