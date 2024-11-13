namespace FiszkiApp.EntityClasses.Models
{
    public class UserDetails
    {
        public int ID_User { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public byte[]? Avatar { get; set; }
        public bool IsAcceptedPolicy { get; set; }
    }

}