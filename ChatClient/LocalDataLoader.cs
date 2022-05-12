using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient
{
    internal class LocalDataLoader : IDataLoader
    {
        public List<User> GetFriends(User user)
        {
            var friends = new List<User>();
            var connectionString = @"data source=localhost\SQLEXPRESS;initial catalog = Chat; persist security info = True;Integrated Security = SSPI;";
            //var connectionString = @"data source=localhost\SQLEXPRESS;initial catalog=Chat;User Id=DESKTOP-FJH7RAC\Legion;";

            var queryString = $"SELECT * FROM dbo.Users";
            using var sqlConnection = new SqlConnection(connectionString);

            var sqlCommand = new SqlCommand(queryString, sqlConnection);
            sqlConnection.Open();

            SqlDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                var id = reader["Id"];
                var name = reader["Name"];
                var lastSeen = reader["LastSeen"];

                friends.Add(new() { Id = (int)id, Name = name.ToString(), LastSeen = (DateTime)lastSeen });
            }

            return friends;
        }

        public List<Message> GetMessages(User user1, User user2)
        {
            throw new NotImplementedException();
        }
    }
}
