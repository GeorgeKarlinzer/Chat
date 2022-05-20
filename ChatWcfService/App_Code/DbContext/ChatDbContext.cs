using ChatData;
using System.Data.Entity;

/// <summary>
/// Summary description for DbContext
/// </summary>
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
}