using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chartful
{
    class Information
    {
        string doc_name { get; set; }
        string content { get; set; }

        public Information(string _doc_name, string _content)
        {
            doc_name = _doc_name;
            content = _content;
        }
    }
}
