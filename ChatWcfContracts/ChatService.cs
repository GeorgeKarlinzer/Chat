using ChatData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ChatWcfContracts
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single)]
    public class ChatService : IChatService
    {
        public void Connect(string username, string password)
        {
            throw new NotImplementedException();
        }

        public List<User> GetFriends(User user)
        {
            throw new NotImplementedException();
        }

        public List<Message> GetMessages(User user1, User user2)
        {
            throw new NotImplementedException();
        }

        public User Login(UserCredentials credentials)
        {
            throw new NotImplementedException();
        }

        public void SendMessage(Message message)
        {
            throw new NotImplementedException();
        }
    }
}
