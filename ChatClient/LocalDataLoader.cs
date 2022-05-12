using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatClient.Dto;

namespace ChatClient
{
    internal class LocalDataLoader : IDataLoader
    {
        private const string connectionString = @"data source=localhost\SQLEXPRESS;initial catalog = Chat; persist security info = True;Integrated Security = SSPI;";
        public List<User> GetFriends(User user)
        {
            var friends = new List<User>();

            var queryString = $"SELECT Id, Name, LastSeen FROM Users WHERE Id IN(SELECT UserId_1 FROM Friends WHERE UserId_2 = 1) OR Id IN(SELECT UserId_2 FROM Friends WHERE UserId_1 = 1)";
            using var sqlConnection = new SqlConnection(connectionString);

            var sqlCommand = new SqlCommand(queryString, sqlConnection);
            sqlConnection.Open();

            SqlDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                var friend = new User
                {
                    Id = (int)reader["Id"],
                    Name = (string)reader["Name"],
                    LastSeen = DateTime.TryParse(reader["LastSeen"] as string, out DateTime temp) ? temp : null
                };

                friends.Add(friend);
            }

            return friends;
        }

        public List<Message> GetMessages(User user1, User user2)
        {
            throw new NotImplementedException();
        }

        public User Login(UserCredentials credentials)
        {
            var user = new User();

            var queryString = $"SELECT Id, Name, LastSeen FROM Users INNER JOIN dbo.UserCredentials ON UserId = Id WHERE UserName = '{credentials.UserName}'";

            using var sqlConnection = new SqlConnection(connectionString);

            var sqlCommand = new SqlCommand(queryString, sqlConnection);
            sqlConnection.Open();

            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.Read())
            {
                user.Id = (int)reader["Id"];
                user.Name = (string)reader["Name"];
                var lastSeen = reader["LastSeen"];
                user.LastSeen = DateTime.Parse(lastSeen.ToString());
                return user;
            }
            else
                return null;

        }
    }
}
