using Chartful.BLL;
using Chartful.BLL.p2p;
using Chartful.Model;
using Chartful.Pages;
using FirstFloor.ModernUI.Windows.Controls;

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

            DocumentsManager = new Manager();
            MyPeerChannel = new PeerChannel();
            PeerChannel.myUpdateDelegate = new UpdateDelegate(this.Receive);

            InitializeComponent();
        }


        /// <summary>
        /// Mise à jour de l'affichage à la reception de données
        /// </summary>
        /// <param name="o"></param>
        public void Receive(Data data)
        {
            this.DocumentsManager.Set(data);
        }
    }
}
