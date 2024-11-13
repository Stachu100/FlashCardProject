namespace FiszkiApp.EntityClasses.Models
{
    public class Category
    {
        public int ID_Category { get; set; }
        public int UserID { get; set; } // dla listy
        public string CategoryName { get; set; }
        public string FrontLanguage { get; set; }
        public string BackLanguage { get; set; }
        public string? LanguageLevel { get; set; }
    }

}