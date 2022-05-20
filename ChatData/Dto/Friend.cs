using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
