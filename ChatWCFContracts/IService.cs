using ChatData;
using System.Collections.Generic;
using System.ServiceModel;

namespace ChatWCFContracts
{
    [ServiceContract(CallbackContract = typeof(IServiceCallback))]
    public interface IService
    {
        [OperationContract]
        User Login(string username, byte[] passwordHash);

        [OperationContract]
        bool Register(string username, byte[] passwordHash, string name, byte[] image);

        [OperationContract(IsOneWay = true)]
        void SendMessage(Message message);

        [OperationContract]
        List<User> GetFriends(User user);

        [OperationContract]
        List<Message> GetMessages(User user1, User user2);

        [OperationContract]
        bool AddFriend(User user, string username);

        [OperationContract]
        void ListenForNewMessages(User user);
    }
}