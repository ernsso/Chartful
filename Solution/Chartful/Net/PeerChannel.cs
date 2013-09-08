using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Chartful.Model;

namespace Chartful.Net.P2P
{
    public delegate void UpdateDelegate(UIObject data);
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
            broadcastChannel.Dispose();
            factory.Close();
            serviceHost.Close();
        }

        public void Send(UIObject data)
        {
            broadcastChannel.sendUIObject(data);
        }

        public void Send(string data)
        {
            broadcastChannel.sendString(data);
        }

        #region PeerNode event handlers
        
        /// <summary>
        /// Evenement se déclanchant lorsque la pair est connectée.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnOnline(object sender, EventArgs e)
        {
            Console.WriteLine(String.Format("The mesh is online !"));
            //Console.WriteLine(e.ToString());
            //Console.WriteLine(sender.GetType());
        }

        /// <summary>
        /// Evenement se déclanchant lorsque la pair est connectée.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnOffline(object sender, EventArgs e)
        {
            Console.WriteLine(String.Format("The mesh is offline !"));
        }

        #endregion
    }
}