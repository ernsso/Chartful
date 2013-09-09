using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chartful
{
    class User
    {
        string name { get; set; }
        string ip { get; set; }

        public User(string _name, string _ip)
        {
            name = _name;
            ip = _ip;
        }
    }
}
