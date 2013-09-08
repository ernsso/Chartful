using Chartful.BLL;
using Chartful.BLL.p2p;
using Chartful.Model;
using Chartful.Pages;
using FirstFloor.ModernUI.Windows.Controls;
using System.Collections.Generic;

namespace Chartful
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
        
    public partial class MainWindow : ModernWindow
    {
        public static MainWindow Current { get; private set; } 
        public PeerChannel MyPeerChannel { get; set; }
        public Manager DocumentsManager { get; private set; }

        public Workspace Editor { get; set; }

        public MainWindow()
        {
            Current = this;

            this.DocumentsManager = new Manager();
            this.MyPeerChannel = new PeerChannel();
            PeerChannel.dataSenderDelegate = new DataSenderDelegate(this.DataReceiver);
            PeerChannel.stringSenderDelegate = new StringSenderDelegate(this.StringReceiver);
            PeerChannel.stringListSenderDelegate = new StringListSenderDelegate(this.StringListReceiver);

            InitializeComponent();
        }


        /// <summary>
        /// Data Recivers
        /// </summary>
        /// <param name="data"></param>
        public void DataReceiver(Data data)
        {
            this.DocumentsManager.Set(data);
        }

        public void StringReceiver(string data)
        {
            if ("-get docNames" == data)
                this.MyPeerChannel.SendStringList(this.DocumentsManager.SharedDocumentsNames);
            else
            {
                var command = data.Split(' ');

                if ("-add" == command[0])
                    this.DocumentsManager.SharedDocumentsNames.Add(command[1]);
                else if ("-del" == command[0])
                    this.DocumentsManager.SharedDocumentsNames.Remove(command[1]);
            }
        }

        public void StringListReceiver(List<string> data)
        {
            foreach (var name in data)
                if (!this.DocumentsManager.SharedDocumentsNames.Contains(name))
                    this.DocumentsManager.SharedDocumentsNames.Add(name);
        }
    }
}
