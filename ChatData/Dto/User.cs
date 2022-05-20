using System;
using System.ComponentModel.DataAnnotations;


namespace ChatData
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? LastSeen { get; set; }
        public byte[] ProfileImage { get; set; }
    }
}
