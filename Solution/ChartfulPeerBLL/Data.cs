using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Chartful.BLL.p2p
{
    [DataContract]
    public class Data
    {
        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public string DocumentName { get; set; }

        [DataMember]
        public string UserId { get; set; }

        [DataMember]
        public string PropertyName { get; set; }

        [DataMember]
        public string Value { get; set; }

        [DataMember]
        public string ObjectName { get; set; }
    }
}
