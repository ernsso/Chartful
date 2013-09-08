using Chartful;
using Chartful.BLL.p2p;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleIHM
{
    class Program
    {
        static void Main(string[] args)
        {
            var manager = new Manager();

            while (true)
            {
                var data = new Data()
                    {
                        DocumentName = "No Id",
                        PropertyName = "No Name",
                        Value = Console.ReadLine()
                    };
                manager.MyPeerChannel.SendData(data);
            }
        }
    }

    class Manager
    {
        public PeerChannel MyPeerChannel { get; set; }

        public Manager()
        {
            MyPeerChannel = new PeerChannel();
            PeerChannel.dataSenderDelegate = new DataSenderDelegate(Receive);
        }

        /// <summary>
        /// Mise à jour de l'affichage à la reception de données
        /// </summary>
        /// <param name="o"></param>
        public void Receive(Data data)
        {
            Console.WriteLine(data.Value);
        }
    }
}
