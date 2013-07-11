using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace Chartful.BLL
{
    [ServiceContract(Namespace = "http://Chartful.Peer")]
    public interface IChartful<T>
    {
        [OperationContract(IsOneWay = true)]
        void sendData(T data);
    }

    public interface IChartfulChannel<T> : IChartful<T>, IClientChannel
    {
    }
}
