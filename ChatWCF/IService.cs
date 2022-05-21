using ChatData;
using System.Collections.Generic;
using System.ServiceModel;

namespace ChatWCFService
{
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        User Login(string username, byte[] passwordHash);

        [OperationContract]
        bool Register(string username, byte[] passwordHash, string name, byte[] image);

        [OperationContract]
        void SendMessage(Message message);

        [OperationContract]
        List<User> GetFriends(User user);

        [OperationContract]
        List<Message> GetMessages(User user1, User user2);
    }
}