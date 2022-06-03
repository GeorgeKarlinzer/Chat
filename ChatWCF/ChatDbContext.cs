using ChatData;
using System;
using System.Configuration;
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
            //string connectionString = ConfigurationManager.ConnectionStrings[0].ConnectionString;

            Database.Connection.ConnectionString = @"Data Source=ltpol271\SQLEXPRESS;Initial Catalog=Chat;User ID=sa;Password=StandardE80";
            Database.Connection.ConnectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=Chat;Integrated Security=SSPI;";
        }

        public static implicit operator ChatDbContext(ServiceHost v)
        {
            throw new NotImplementedException();
        }
    }
}