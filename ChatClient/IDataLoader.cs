using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient
{
    internal interface IDataLoader
    {
        List<User> GetFriends(User user);
        List<Message> GetMessages(User user1, User user2);
    }
}
