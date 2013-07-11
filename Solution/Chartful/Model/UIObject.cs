using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chartful.Model
{
    public class UIObject
    {
        public string Text { get; set; }
        public string UIType { get; set; }
        public double FontSize { get; set; }
        public double Top { get; set; }
        public double Left { get; set; }

        public UIObject() { }
        public UIObject(string _text,string _type, string _fontsize, string _top, string _left)
        {
            Text = _text;
            UIType = _type;
            FontSize = Convert.ToDouble(_fontsize);
            Top = Convert.ToDouble(_top);
            Left = Convert.ToDouble(_left);
        }

    }
}
