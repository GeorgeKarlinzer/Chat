using ChatData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service" in code, svc and config file together.
public class Service : IService
{
    public List<User> GetFriends(User user)
    {
        throw new NotImplementedException();
    }

    public List<Message> GetMessages(User user1, User user2)
    {
        throw new NotImplementedException();
    }

    public User Login(string username, string password)
    {
        throw new NotImplementedException();
    }

    public void SendMessage(Message message)
    {
        throw new NotImplementedException();
    }
}
