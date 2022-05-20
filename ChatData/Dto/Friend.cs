using System.ComponentModel.DataAnnotations;

namespace ChatData
{
    public class Friend
    {
        [Key]
        public int Id { get; set; }
        public int UserId_1 { get; set; }
        public int UserId_2 { get; set; }
    }
}
