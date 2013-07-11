using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace Chartful.BLL
{
    public class PeerChannel
    {
        private ServiceHost serviceHost;
        private ChannelFactory<IChartfulChannel<string>> factory;
        private IChartfulChannel<string> broadcastChannel;

        public PeerChannel()
        {
            serviceHost = new ServiceHost(typeof(PeerReceiver<string>));
            // Open the ServiceHostBase to create listeners and start listening for messages.
            serviceHost.Open();

            EndpointAddress lookFor = new EndpointAddress(serviceHost.BaseAddresses[0].ToString() + "tchat");

            factory = new ChannelFactory<IChartfulChannel<string>>("BroadcastEndpoint", lookFor);

            broadcastChannel = factory.CreateChannel();

            //IOnlineStatus ostat = broadcastChannel.GetProperty<IOnlineStatus>();
            //ostat.Online += new EventHandler(OnOnline);
            //ostat.Offline += new EventHandler(OnOffline);

            broadcastChannel.Open();
            string msg = "Wellcome ";
            broadcastChannel.sendData(msg);
        }

        ~PeerChannel()
        {
            broadcastChannel.Dispose();
            factory.Close();
            serviceHost.Close();
        }

        public void Send(string data)
        {
            broadcastChannel.sendData(data);
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