using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatData;

namespace ChatClient
{
    internal interface IDataService
    {
        List<User> GetFriends(User user);
        List<Message> GetMessages(User user1, User user2);
        void SendMessage(Message message);
        User Login(string username, string password);
        bool Register(string username, string password, string name, byte[] image);
    }
}
