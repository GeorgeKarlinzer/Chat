using ChatData;
using System.ServiceModel;

namespace ChatWCFContracts
{
    [ServiceContract]
    public interface IServiceCallback
    {
        [OperationContract(IsOneWay = true)]
        void GetNewMessage(Message message);
    }
}
