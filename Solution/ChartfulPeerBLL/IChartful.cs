using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace Chartful.BLL.p2p
{
    [ServiceContract(Namespace = "http://Chartful.Peer")]
    public interface IChartful
    {
        [OperationContract(IsOneWay = true)]
        void sendUIObject(UIObject data);

        [OperationContract(IsOneWay = true)]
        void sendString(string data);
    }

    public interface IChartfulChannel : IChartful, IClientChannel
    {
    }
}
