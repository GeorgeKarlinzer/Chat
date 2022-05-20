using ChatData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

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
