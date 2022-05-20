using System;
using System.ComponentModel.DataAnnotations;

namespace ChatData
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
    }
}
