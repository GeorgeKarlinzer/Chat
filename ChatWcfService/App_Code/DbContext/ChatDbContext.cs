using ChatData;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

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

    }
}