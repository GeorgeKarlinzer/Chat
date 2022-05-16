using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatData;

namespace ChatHostService.DbContexts
{
    public class ChatDbContext : DbContext
    {
        public ChatDbContext() : base("name=ChatDbContext")
        {

        }


        public virtual DbSet<User> 
    }
}
