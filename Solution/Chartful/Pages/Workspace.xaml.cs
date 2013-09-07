using Chartful.BLL;
using Chartful.Controls;
using Chartful.Model;
using FirstFloor.ModernUI.Windows.Controls;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Chartful.Pages
{
    /// <summary>
    /// Interaction logic for Workspace.xaml
    /// </summary>
    public partial class Workspace : UserControl
    {
        MainWindow mainWindow;

        public ObservableCollection<Document> Documents { get; private set; }
        public ObservableCollection<UIElement> Elements { get; set; }

        public Document Selected { get; set; }

        #region Constructors
        public Workspace()
        {
            InitializeComponent();
            DataContext = this;

            mainWindow = Application.Current.MainWindow as MainWindow;
        }
        #endregion
    }
}
