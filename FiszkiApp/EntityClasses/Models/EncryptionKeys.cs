namespace FiszkiApp.EntityClasses.Models
{
    public class EncryptionKeys
    {
        public int ID_User { get; set; }
        public byte[] EncryptionKey { get; set; }
        public byte[] IV { get; set; }
    }
}