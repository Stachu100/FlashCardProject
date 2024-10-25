namespace FiszkiApp.EntityClasses.Models
{
    public class User
    {
        public int ID_User { get; set; }
        public string UserName { get; set; }
        public byte[] UserPassword { get; set; }
    }
}
