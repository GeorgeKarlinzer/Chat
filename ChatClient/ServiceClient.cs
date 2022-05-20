﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.9151
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


using ChatClient;

[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
[System.ServiceModel.ServiceContractAttribute(ConfigurationName = "IService")]
public interface IService
{
    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IService/Login", ReplyAction = "http://tempuri.org/IService/LoginResponse")]
    ChatData.User Login(string username, string password);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IService/Register", ReplyAction = "http://tempuri.org/IService/RegisterResponse")]
    bool Register(string username, string password, string name, byte[] image);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IService/SendMessage", ReplyAction = "http://tempuri.org/IService/SendMessageResponse")]
    void SendMessage(ChatData.Message message);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IService/GetFriends", ReplyAction = "http://tempuri.org/IService/GetFriendsResponse")]
    ChatData.User[] GetFriends(ChatData.User user);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IService/GetMessages", ReplyAction = "http://tempuri.org/IService/GetMessagesResponse")]
    ChatData.Message[] GetMessages(ChatData.User user1, ChatData.User user2);
}

[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
public interface IServiceChannel : IService, System.ServiceModel.IClientChannel
{
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
public partial class ServiceClient : System.ServiceModel.ClientBase<IService>, IService
{

    public ServiceClient()
    {
    }

    public ServiceClient(string endpointConfigurationName) :
            base(endpointConfigurationName)
    {
    }

    public ServiceClient(string endpointConfigurationName, string remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
    {
    }

    public ServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
    {
    }

    public ServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
    {
    }

    public ChatData.User Login(string username, string password)
    {
        return base.Channel.Login(username, password);
    }

    public bool Register(string username, string password, string name, byte[] image)
    {
        return base.Channel.Register(username, password, name, image);
    }

    public void SendMessage(ChatData.Message message)
    {
        base.Channel.SendMessage(message);
    }

    public ChatData.User[] GetFriends(ChatData.User user)
    {
        return base.Channel.GetFriends(user);
    }

    public ChatData.Message[] GetMessages(ChatData.User user1, ChatData.User user2)
    {
        return base.Channel.GetMessages(user1, user2);
    }
}
