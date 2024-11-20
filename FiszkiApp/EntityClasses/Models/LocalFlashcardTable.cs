using SQLite;

namespace FiszkiApp.EntityClasses.Models
{
    public class LocalFlashcardTable
    {
        [PrimaryKey, AutoIncrement]
        public int ID_Flashcard { get; set; }
        public int IdCategory { get; set; }
        public string FrontFlashCard { get; set; }
        public string BackFlashCard { get; set; }

        [Ignore]
        public int Lp { get; set; }
    }
}