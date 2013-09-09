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
            PeerChannel.dataSenderDelegate.Invoke(data);
        }

        public void SendString(string data)
        {
            PeerChannel.stringSenderDelegate.Invoke(data);
        }

        public void SendStringList(List<string> data)
        {
            PeerChannel.stringListSenderDelegate.Invoke(data);
        }
    }
}
