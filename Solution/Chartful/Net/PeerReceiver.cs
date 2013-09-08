using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Runtime.Serialization;
using Chartful.Model;

namespace Chartful.Net.P2P
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    class PeerReceiver : IChartful
    {
        public void sendUIObject(UIObject data)
        {
            PeerChannel.myUpdateDelegate.Invoke(data);
        }

        public void sendString(string data)
        {
            PeerChannel.myTestDelegate.Invoke(data);
        }
    }
}
