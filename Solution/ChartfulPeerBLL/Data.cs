using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Chartful
{
    [DataContract]
    public class Data
    {
        [DataMember]
        public string DocumentID { get; set; }

        [DataMember]
        public string PropertyName { get; set; }

        [DataMember]
        public string Value { get; set; }

        [DataMember]
        public DateTime ModifyDate { get; set; }

    }
}
