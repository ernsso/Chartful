using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace Chartful.BLL
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    class PeerReceiver<T> : IChartful<T>
    {
        public void sendData(T data)
        {
            throw new NotImplementedException();
        }
    }
}
