using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.PeerResolvers;
using System.ServiceModel.Description;
using System.Configuration;

namespace Chartful.Net.P2P
{
    class MeshManager
    {
        private Dictionary<string, ServiceHost> serviceHostDictionary;
        private Dictionary<string, IChartfulChannel> chartfulChannelDictionary;
        private Dictionary<string, ChannelFactory<IChartfulChannel>> channelFactoryDictionary;

        private static MeshManager instance;
        
        public static MeshManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new MeshManager();
                return instance;
            }
        }
        
        private MeshManager()
        {
            this.serviceHostDictionary = new Dictionary<string, ServiceHost>();
            this.chartfulChannelDictionary = new Dictionary<string, IChartfulChannel>();
        }

        ~MeshManager()
        {
            foreach (KeyValuePair<string, IChartfulChannel> kvp in this.chartfulChannelDictionary)
                kvp.Value.Dispose();
            foreach (KeyValuePair<string, ChannelFactory<IChartfulChannel>> kvp in this.channelFactoryDictionary)
                kvp.Value.Close();
            foreach (KeyValuePair<string, ServiceHost> kvp in this.serviceHostDictionary)
                kvp.Value.Close();
        }

        /// <summary>
        /// Connecte le client à une maille.
        /// </summary>
        /// <param name="meshName">Le nom de la maille.</param>
        /// <returns>Retourne la chaine qui permettra au client de communiquer</returns>
        public IChartfulChannel SignInMesh(string meshName = "PublicMesh")
        {
            // Création et configuration du binding 
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

            string address = ConfigurationManager.AppSettings["baseAddress"] + meshName;
            EndpointAddress lookFor = new EndpointAddress(address);
            
            //ServiceEndpoint serviceEndpoint = new ServiceEndpoint(new ContractDescription("ChatP2P"), netPeerTcpBinding, lookFor);
            ServiceHost serviceHost = new ServiceHost(typeof(PeerReceiver));
            serviceHost.AddServiceEndpoint(typeof(IChartful), netPeerTcpBinding, address);
            serviceHostDictionary.Add(meshName, serviceHost);

            // Ouverture du servicehost pour créer les listeners et débuter l'écoute des message.
            serviceHost.Open();

            ChannelFactory<IChartfulChannel> factory = new ChannelFactory<IChartfulChannel>(netPeerTcpBinding, lookFor);
            channelFactoryDictionary.Add(meshName, factory);

            //Création de la chaine
            IChartfulChannel newChannel = factory.CreateChannel();
            chartfulChannelDictionary.Add(meshName, newChannel);

            return newChannel;
        }

        /// <summary>
        /// Déconnecte le client d'une maille.
        /// </summary>
        /// <param name="meshName">Nom de la maille</param>
        public void SignOutMesh(string meshName)
        {
            this.CloseChartfulChannel(meshName);
            this.CloseChannelFactory(meshName);
            this.CloseServiceHost(meshName);
        }

        private void CloseServiceHost(string meshName)
        {
            if (String.IsNullOrEmpty(meshName))
            {
                ServiceHost sh;
                if (this.serviceHostDictionary.TryGetValue(meshName, out sh))
                {
                    sh.Close();
                    this.channelFactoryDictionary.Remove(meshName);
                }
            }
        }

        private void CloseChannelFactory(string meshName)
        {
            if (String.IsNullOrEmpty(meshName))
            {
                ChannelFactory<IChartfulChannel> cf;
                if (this.channelFactoryDictionary.TryGetValue(meshName, out cf))
                {
                    cf.Close();
                    this.channelFactoryDictionary.Remove(meshName);
                }
            }
        }
        
        private void CloseChartfulChannel(string meshName)
        {
            if (String.IsNullOrEmpty(meshName))
            {
                IChartfulChannel cc;
                if (this.chartfulChannelDictionary.TryGetValue(meshName, out cc))
                {
                    cc.Dispose();
                    this.channelFactoryDictionary.Remove(meshName);
                }
            }
        }
    }
}
