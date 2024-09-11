namespace FiszkiApp.Models;

public class Category
{
    public int Id { get; set; }
    public string Url { get; set; }
    public int CategoryID { get; set; }
    public string CategoryName { get; set; }
    public string FrontLanguage { get; set; }
    public string BackLanguage { get; set; }
    public string FrontFlagUrl { get; set; }
    public string BackFlagUrl { get; set; }
}
