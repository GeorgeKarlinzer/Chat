using ChatData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient
{
    public class RemoteDataService : IDataService
    {
        public bool AddFriend(User user, string username)
        {
            var serviceClient = ServiceClient.GetConfiguredClient();

            return serviceClient.AddFriend(user, username);
        }

        public List<User> GetFriends(User user)
        {
            var serviceClient = ServiceClient.GetConfiguredClient();

            return serviceClient.GetFriends(user).ToList();
        }

        public List<Message> GetMessages(User user1, User user2)
        {
            var serviceClient = ServiceClient.GetConfiguredClient();

            return serviceClient.GetMessages(user1, user2).ToList();
        }

        public User Login(string username, string password)
        {
            var serviceClient = ServiceClient.GetConfiguredClient();

            var encryption = new Encryption();

            byte[] salt = encryption.GenerateSalt(username);
            byte[] passwordHash = encryption.ComputeSaltedHash(password, salt);

            return serviceClient.Login(username, passwordHash);
        }

        public bool Register(string username, string password, string name, byte[] image)
        {
            var serviceClient = ServiceClient.GetConfiguredClient();

            var encryption = new Encryption();

            var salt = encryption.GenerateSalt(username);
            var passwordHash = encryption.ComputeSaltedHash(password, salt);

            return serviceClient.Register(username, passwordHash, name, image);
        }

        public void SendMessage(Message message)
        {
            var serviceClient = ServiceClient.GetConfiguredClient();

            serviceClient.SendMessage(message);
        }
    }
}
