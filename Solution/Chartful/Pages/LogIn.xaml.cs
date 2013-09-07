using FirstFloor.ModernUI.Presentation;
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

namespace Chartful.Pages
{
    /// <summary>
    /// Interaction logic for LogIn.xaml
    /// </summary>
    public partial class LogIn : UserControl
    {
        MainWindow mainWindow;

        public LogIn()
        {
            InitializeComponent();
            mainWindow = Application.Current.MainWindow as MainWindow;
        }

        private void SignIn_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.DocumentId.Text))
            {
                mainWindow.DocumentsManager.OpenFile(this.DocumentId.Text);

                NavigationCommands.GoToPage.Execute("/Pages/Workspace.xaml", null);
            }
        }
    }
}
