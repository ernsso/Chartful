using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace Chartful.BLL.p2p
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    class PeerReceiver : IChartful
    {
        public void SendData(Data data)
        {
            PeerChannel.myUpdateDelegate.Invoke(data);
        }

        public void SendString(string data)
        {
            PeerChannel.myTestDelegate.Invoke(data);
        }
    }
}
