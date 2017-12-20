using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MessagingServer
{
    
        [DataContract]
        public class OnlineOrder
        {
            [DataMember]
            public string Customer { get; set; }
            [DataMember]
            public string Product { get; set; }
        }
    
}
