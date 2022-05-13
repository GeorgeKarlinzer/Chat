using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatData;

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
            // TODO: Not get all messages
            var messages = new List<Message>();
            var sqlConnection = new SqlConnection(connectionString);
            var sqlQuery = $"SELECT SenderId, ReceiverId, Text, Date FROM Messages WHERE (SenderId = {user1.Id} AND ReceiverId = {user2.Id}) OR (SenderId = {user2.Id} AND ReceiverId = {user1.Id}) ORDER BY Date";

            var sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
            sqlConnection.Open();

            SqlDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                var message = new Message
                {
                    SenderId = (int)reader["SenderId"],
                    ReceiverId = (int)reader["ReceiverId"],
                    Text = (string)reader["Text"],
                    Date = (DateTime)reader["Date"]
                };

                messages.Add(message);
            }

            return messages;
        }

        public User Login(UserCredentials credentials)
        {

            var queryString = $"SELECT Id, Name, LastSeen FROM Users INNER JOIN dbo.UserCredentials ON UserId = Id WHERE UserName = '{credentials.UserName}'";

            using var sqlConnection = new SqlConnection(connectionString);

            var sqlCommand = new SqlCommand(queryString, sqlConnection);
            sqlConnection.Open();

            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.Read())
            {
                var user = new User
                {
                    Id = (int)reader["Id"],
                    Name = (string)reader["Name"],
                    LastSeen = reader["LastSeen"] as DateTime?
                };
                return user;
            }
            else
            {
                return null;
            }

        }

        public void SendMessage(Message message)
        {
            message.Date = DateTime.Now;

            var queryString = $"INSERT INTO Messages (SenderId, ReceiverId, Text, Date) VALUES ({message.SenderId}, {message.ReceiverId}, N'{message.Text}', '{message.Date:yyyy-MM-dd HH:mm:ss}')";
            using var sqlConnection = new SqlConnection(connectionString);
            var sqlCommand = new SqlCommand(queryString, sqlConnection);
            sqlConnection.Open();
            sqlCommand.ExecuteReader();
        }
    }
}
