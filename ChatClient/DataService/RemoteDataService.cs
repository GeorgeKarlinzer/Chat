using ChatData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ChatClient
{
    public class RemoteDataService : IDataService
    {
        public bool AddFriend(User user, string username)
        {
            var serviceClient = ServiceClient.GetConfiguredClient();

            bool v = serviceClient.AddFriend(user, username);

            serviceClient.Close();

            return v;
        }

        public List<User> GetFriends(User user)
        {
            var serviceClient = ServiceClient.GetConfiguredClient();

            List<User> friends = serviceClient.GetFriends(user).ToList();

            serviceClient.Close();

            return friends;
        }

        public List<Message> GetMessages(User user1, User user2)
        {
            var serviceClient = ServiceClient.GetConfiguredClient();

            List<Message> messages = serviceClient.GetMessages(user1, user2).ToList();

            serviceClient.Close();

            return messages;
        }

        public User Login(string username, string password)
        {
            var serviceClient = ServiceClient.GetConfiguredClient();

            var encryption = new Encryption();

            byte[] salt = encryption.GenerateSalt(username);
            byte[] passwordHash = encryption.ComputeSaltedHash(password, salt);

            User user = serviceClient.Login(username, passwordHash);

            serviceClient.Close();

            return user;
        }

        public bool Register(string username, string password, string name, byte[] image)
        {
            var serviceClient = ServiceClient.GetConfiguredClient();

            var encryption = new Encryption();

            var salt = encryption.GenerateSalt(username);
            var passwordHash = encryption.ComputeSaltedHash(password, salt);

            bool v = serviceClient.Register(username, passwordHash, name, image);

            serviceClient.Close();

            return v;
        }

        public void SendMessage(Message message)
        {
            var serviceClient = ServiceClient.GetConfiguredClient();

            serviceClient.SendMessage(message);
            
            serviceClient.Close();
        }
    }
}
