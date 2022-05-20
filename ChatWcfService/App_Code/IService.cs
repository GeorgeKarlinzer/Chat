using ChatData;
using System.Collections.Generic;
using System.ServiceModel;

[ServiceContract]
public interface IService
{
	[OperationContract]
	User Login(string username, string password);

    [OperationContract]
    bool Register(string username, string password, string name, byte[] image);

    [OperationContract]
    void SendMessage(Message message);

    [OperationContract]
    List<User> GetFriends(User user);

    [OperationContract]
    List<Message> GetMessages(User user1, User user2);
}
