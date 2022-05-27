using ChatData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ChatWCFContracts
{
    [ServiceContract(Name = nameof(IService))]
    public interface IClientService : IService
    {
        [OperationContract]
        Task ListenForNewMessagesAsync(User user);
    }
}
