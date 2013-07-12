using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Chartful.BLL
{
    [DataContract]
    public class UIObject
    {
		[DataMember]
        public string ID { get; set; }
        [DataMember]
        public string UIType { get; set; }
        [DataMember]
        public string Content { get; set; }
        [DataMember]
        public double FontSize { get; set; }
        [DataMember]
        public double Top { get; set; }
        [DataMember]
        public double Left { get; set; }
		
        public UIObject() { }
        public UIObject(string _id, string _text,string _type, string _fontsize, string _top, string _left)
        {
            ID = _id;
            Content = _text;
            UIType = _type;
            FontSize = Convert.ToDouble(_fontsize);
            Top = Convert.ToDouble(_top);
            Left = Convert.ToDouble(_left);
        }
    }
}
