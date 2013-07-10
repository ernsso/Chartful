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
using System.Collections;
using Chartful.Model;
using FirstFloor.ModernUI.Presentation;
using System.Windows.Controls.Primitives;
using Microsoft.Win32;
using System.IO;
using FirstFloor.ModernUI.Windows.Controls;
using System.Collections.ObjectModel;

namespace Chartful.Pages
{
    /// <summary>
    /// Interaction logic for Starting.xaml
    /// </summary>

    public partial class Starting : UserControl
    {
        ObservableCollection<Document> documents;
        public ObservableCollection<Document> Documents
        {
            get { return documents; }
        }

        public Starting()
        {
            documents = new ObservableCollection<Document>();

            for(int i = 0; i < 10; i++)
                documents.Add(new Document(@"C:\www\chartful\test"+i+".ctf"));

            InitializeComponent();
            DataContext = this;
        }

        private void open_Click(object sender, RoutedEventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.InitialDirectory = "c:\\";
            ofd.Filter = "Chartful files (*.ctf)|*.ctf|All files (*.*)|*.*";

            Nullable<bool> result = ofd.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                var wnd = Application.Current.MainWindow as MainWindow;
                wnd.DocManager.Documents.Add(new Document(ofd.FileName));
                wnd.DocManager.SelectLastDocument();

                NavigationCommands.GoToPage.Execute("/Pages/Workspace.xaml", null);
            }
        }

        private void new_Click(object sender, RoutedEventArgs e)
        {
            
            var sfd = new SaveFileDialog();
            sfd.InitialDirectory = "c:\\";
            sfd.Filter = "Chartful files (*.ctf)|*.ctf|All files (*.*)|*.*";
            sfd.FileName = "New Document"; // Default file name
            sfd.DefaultExt = ".ctf";

            Nullable<bool> result = sfd.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                var wnd = Application.Current.MainWindow as MainWindow;
                wnd.DocManager.Documents.Add(new Document(sfd.FileName));
                wnd.DocManager.SelectLastDocument();
                
                File.Create(sfd.FileName);

                NavigationCommands.GoToPage.Execute("/Pages/Workspace.xaml", null);
            }
        }

        protected void HandleDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var path = ((Document)((ListViewItem)sender).Content).Name;
            var doc = new Document(path);

            var wnd = Application.Current.MainWindow as MainWindow;
            wnd.DocManager.Documents.Add(doc);
            wnd.DocManager.SelectLastDocument();
            
            NavigationCommands.GoToPage.Execute("/Pages/Workspace.xaml", null);
        }
    }
}
