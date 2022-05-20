using ChatData;
using System;
using System.Data.Entity;
using System.ServiceModel;

namespace ChatWCFService
{
    public class ChatDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Friend> Friends { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<UserCredentials> UserCredentials { get; set; }


        public ChatDbContext()
        {
            string connectionString = System.Configuration.ConfigurationManager.
                ConnectionStrings["defaultConnection"].ConnectionString;

            Database.Connection.ConnectionString = connectionString;
        }

        public static implicit operator ChatDbContext(ServiceHost v)
        {
            throw new NotImplementedException();
        }
    }
}