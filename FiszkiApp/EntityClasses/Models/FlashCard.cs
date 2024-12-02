namespace FiszkiApp.EntityClasses.Models
{
    public class FlashCard
    {
        public int ID_flashcard { get; set; }
        public int ID_Category { get; set; }
        public string FrontFlashCard { get; set; }
        public string BackFlashCard { get; set; }
    }
}