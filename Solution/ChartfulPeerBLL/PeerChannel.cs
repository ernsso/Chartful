using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace Chartful.BLL.p2p
{
    public delegate void UpdateDelegate(Data data);
    public delegate void testDelegate(string data);

    public class PeerChannel
    {
        public static UpdateDelegate myUpdateDelegate;
        public static testDelegate myTestDelegate;
        private ServiceHost serviceHost;
        private ChannelFactory<IChartfulChannel> factory;
        private IChartfulChannel broadcastChannel;

        public PeerChannel()
        {
            try
            {
                serviceHost = new ServiceHost(typeof(PeerReceiver));
                // Open the ServiceHostBase to create listeners and start listening for messages.
                serviceHost.Open();

                EndpointAddress lookFor = new EndpointAddress(serviceHost.BaseAddresses[0].ToString() + "broadcast");

                factory = new ChannelFactory<IChartfulChannel>("BroadcastEndpoint", lookFor);

                broadcastChannel = factory.CreateChannel();

                //IOnlineStatus ostat = broadcastChannel.GetProperty<IOnlineStatus>();
                //ostat.Online += new EventHandler(OnOnline);
                //ostat.Offline += new EventHandler(OnOffline);

                broadcastChannel.Open();
            }
            catch (Exception e)
            {
                string ex = e.Message;
            }
        }

        ~PeerChannel()
        {
            if (null != broadcastChannel)
                broadcastChannel.Dispose();
            if (null != factory)
                factory.Close();
            if (null != serviceHost)
                serviceHost.Close();
        }

        public void Send(Data data)
        {
            broadcastChannel.Send(data);
        }

        public void Send(string data)
        {
            broadcastChannel.Send(data);
        }


        // PeerNode event handlers
        private void OnOnline(object sender, EventArgs e)
        {
            Console.WriteLine(String.Format("The mesh is online !"));
            //Console.WriteLine(e.ToString());
            //Console.WriteLine(sender.GetType());
        }

        private void OnOffline(object sender, EventArgs e)
        {
            Console.WriteLine(String.Format("The mesh is offline !"));
        }
    }
}