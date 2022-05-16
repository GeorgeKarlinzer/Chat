using ChatData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ChatWcfContracts
{
    [DataContract]
    public class GetFriendsRequest
    {
        [DataMember]
        public User User { get; set; }
    }
}
