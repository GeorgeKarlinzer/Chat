using ChatData;
using System;
using System.Collections.Generic;
using System.IO;
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
            File.WriteAllText(@"C:\Users\kupryianau.h\Desktop\Text.txt", $"User {username} connected with password {password}");
        }

        public string GetResponse(string text)
        {
            return $"This is the response for {text}";
        }
        //public List<User> GetFriends(User user)
        //{
        //    throw new NotImplementedException();
        //}

        //public List<Message> GetMessages(User user1, User user2)
        //{
        //    throw new NotImplementedException();
        //}

        //public User Login(UserCredentials credentials)
        //{
        //    throw new NotImplementedException();
        //}

        //public void SendMessage(Message message)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
