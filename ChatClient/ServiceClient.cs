using ChatData;
using ChatWCFContracts;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

//[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
//[System.ServiceModel.ServiceContractAttribute(ConfigurationName = "IService")]
//public interface IService
//{
//    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IService/Login", ReplyAction = "http://tempuri.org/IService/LoginResponse")]
//    User Login(string username, byte[] passwordHash);

//    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IService/Register", ReplyAction = "http://tempuri.org/IService/RegisterResponse")]
//    bool Register(string username, byte[] passwordHash, string name, byte[] image);

//    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IService/SendMessage", ReplyAction = "http://tempuri.org/IService/SendMessageResponse")]
//    void SendMessage(Message message);

//    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IService/GetFriends", ReplyAction = "http://tempuri.org/IService/GetFriendsResponse")]
//    User[] GetFriends(User user);

//    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IService/GetMessages", ReplyAction = "http://tempuri.org/IService/GetMessagesResponse")]
//    Message[] GetMessages(User user1, User user2);

//    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IService/AddFriend", ReplyAction = "http://tempuri.org/IService/AddFriendResponse")]
//    bool AddFriend(User user, string username);
//}

[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
public interface IServiceChannel : IService, System.ServiceModel.IClientChannel
{
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
public partial class ServiceClient : DuplexClientBase<IService>, IClientService
{
    public static ServiceClient GetConfiguredClient(InstanceContext instanceContext)
    {
        var endpointAddress = new EndpointAddress("net.tcp://localhost:8090/Service");
        var binding = new NetTcpBinding();
        binding.ReliableSession.InactivityTimeout = new TimeSpan(0, 0, 0, 0, Timeout.Infinite);
        binding.ReliableSession.Enabled = true;
        binding.Name = "NetTcpBinding_IService";

        return new ServiceClient(instanceContext, binding, endpointAddress);
    }

    public ServiceClient(InstanceContext instanceContext) : 
            base(instanceContext)
    {

    }

    public ServiceClient(InstanceContext instanceContext, string endpointConfigurationName) :
            base(instanceContext, endpointConfigurationName)
    {
    }

    public ServiceClient(InstanceContext instanceContext, string endpointConfigurationName, string remoteAddress) :
            base(instanceContext, endpointConfigurationName, remoteAddress)
    {
    }

    public ServiceClient(InstanceContext instanceContext, string endpointConfigurationName, EndpointAddress remoteAddress) :
            base(instanceContext, endpointConfigurationName, remoteAddress)
    {
    }

    public ServiceClient(InstanceContext intstanceContext, System.ServiceModel.Channels.Binding binding, EndpointAddress remoteAddress) :
            base(intstanceContext, binding, remoteAddress)
    {
    }

    public User Login(string username, byte[] passwordHash)
    {
        return base.Channel.Login(username, passwordHash);
    }

    public bool Register(string username, byte[] passwordHash, string name, byte[] image)
    {
        return base.Channel.Register(username, passwordHash, name, image);
    }

    public void SendMessage(Message message)
    {
        base.Channel.SendMessage(message);
    }

    public List<User> GetFriends(User user)
    {
        return base.Channel.GetFriends(user);
    }

    public List<Message> GetMessages(User user1, User user2)
    {
        return base.Channel.GetMessages(user1, user2);
    }

    public bool AddFriend(User user, string username)
    {
        return base.Channel.AddFriend(user, username);
    }

    public Task ListenForNewMessagesAsync(User user)
    {
        return Task.Run(() => ListenForNewMessages(user));
    }

    public void ListenForNewMessages(User user)
    {
        base.Channel.ListenForNewMessages(user);
    }

    public Task ListenForNewFriendsAsync(User user)
    {
        return Task.Run(() => ListenForNewFriends(user));
    }

    public void ListenForNewFriends(User user)
    {
        base.Channel.ListenForNewFriends(user);
    }

}
