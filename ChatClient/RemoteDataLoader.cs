using ChatData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient
{
    public class RemoteDataLoader : IDataLoader
    {
        public List<User> GetFriends(User user)
        {
            var serviceClient = new ServiceClient();

            return serviceClient.GetFriends(user).ToList();
        }

        public List<Message> GetMessages(User user1, User user2)
        {
            var serviceClient = new ServiceClient();

            return serviceClient.GetMessages(user1, user2).ToList();
        }

        public User Login(string username, string password)
        {
            var serviceClient = new ServiceClient();

            return serviceClient.Login(username, password);
        }

        public void SendMessage(Message message)
        {
            var serviceClient = new ServiceClient();

            serviceClient.SendMessage(message);
        }
    }
}
