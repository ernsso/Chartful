using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chartful.Net.p2p
{
    class MeshManager
    {
        public IChartfulChannel CreateChannel(string meshName)
        {
            string address = @"net.p2p://broadcastmesh/TchatService/tchat";
            EndpointAddress lookFor = new EndpointAddress(address);

            NetPeerTcpBinding netPeerTcpBinding = new NetPeerTcpBinding();
            netPeerTcpBinding.Port = 0;
            netPeerTcpBinding.OpenTimeout = new TimeSpan(0, 10, 0);
            netPeerTcpBinding.CloseTimeout = new TimeSpan(0, 10, 0);
            netPeerTcpBinding.SendTimeout = new TimeSpan(0, 10, 0);
            netPeerTcpBinding.ReceiveTimeout = new TimeSpan(0, 10, 0);
            netPeerTcpBinding.MaxReceivedMessageSize = 65536;
            netPeerTcpBinding.MaxBufferPoolSize = 524288;
            netPeerTcpBinding.Security.Mode = SecurityMode.None;
            netPeerTcpBinding.Resolver.Mode = PeerResolverMode.Pnrp;
            netPeerTcpBinding.ReaderQuotas.MaxDepth = 2147483647;
            netPeerTcpBinding.ReaderQuotas.MaxStringContentLength = 2147483647;
            netPeerTcpBinding.ReaderQuotas.MaxArrayLength = 2147483647;
            netPeerTcpBinding.ReaderQuotas.MaxBytesPerRead = 2147483647;
            netPeerTcpBinding.ReaderQuotas.MaxNameTableCharCount = 2147483647;

            ServiceEndpoint serviceEndpoint = new ServiceEndpoint(new ContractDescription("ChatP2P"), netPeerTcpBinding, lookFor);

            serviceHost.AddServiceEndpoint(typeof(IBroadcast), netPeerTcpBinding, address);
            factory = new ChannelFactory<IBroadcastChannel>(netPeerTcpBinding, lookFor);
        }
    }
}