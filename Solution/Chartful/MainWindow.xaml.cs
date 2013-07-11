using Chartful.Model;
using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Chartful.BLL.p2p;
using Chartful.BLL;

namespace Chartful
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ModernWindow
    {
        public PeerChannel MyPeerChannel { get; private set; }
        public Manager DocManager { get; private set; }

        public MainWindow()
        {
            DocManager = new Manager();
            MyPeerChannel = new PeerChannel();
            PeerChannel.myUpdateDelegate = new UpdateDelegate(Refresh);

            InitializeComponent();
        }


        /// <summary>
        /// Mise à jour de l'affichage à la reception de données
        /// </summary>
        /// <param name="o"></param>
        public void Refresh(Chartful.BLL.UIObject o)
        {
            DocManager.Selected.Content.Add((UIObject)o);
        }
    }
}
