using ChatData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ChatWcfContracts
{
    [ServiceContract]
    public interface IChatService
    {
        [OperationContract]
        void Connect(string username, string password);

        [OperationContract]
        string GetResponse(string text);

        //[OperationContract(IsOneWay = false)]
        //void SendMessage(Message message);
        
        //[OperationContract(IsOneWay = false)]
        //User Login(UserCredentials credentials);

        //[OperationContract(IsOneWay = true)]
        //List<User> GetFriends(User user);

        //[OperationContract(IsOneWay = true)]
        //List<Message> GetMessages(User user1, User user2);
    }
}
