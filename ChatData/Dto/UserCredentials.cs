using System.ComponentModel.DataAnnotations;

namespace ChatData
{
    public class UserCredentials
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] Salt { get; set; }
    }
}
