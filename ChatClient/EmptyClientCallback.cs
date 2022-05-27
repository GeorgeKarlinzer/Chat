using ChatData;
using ChatWCFContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient
{
    internal class EmptyClientCallback : IServiceCallback
    {
        public void GetNewMessage(Message message)
        {
            throw new NotImplementedException();
        }
    }
}
