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

            //Tests for the recent files list
            //for(int i = 0; i < 10; i++)
            //    documents.Add(new Document(@"C:\www\chartful\test"+i+".ctf"));

            InitializeComponent();
            DataContext = this;
        }

        /// <summary>
        /// Show the files explorer to open a file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void open_Click(object sender, RoutedEventArgs e)
        {
            var wnd = Application.Current.MainWindow as MainWindow;
            bool opened = wnd.DocumentsManager.OpenFile();

            if (opened)
                NavigationCommands.GoToPage.Execute("/Pages/Workspace.xaml", null);
        }

        /// <summary>
        /// Show the files explorer to create a new file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void new_Click(object sender, RoutedEventArgs e)
        {    
            var wnd = Application.Current.MainWindow as MainWindow;
            bool created = wnd.DocumentsManager.NewFile();

            if(created)
                NavigationCommands.GoToPage.Execute("/Pages/Workspace.xaml", null);
        }

        protected void HandleDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var path = ((Document)((ListViewItem)sender).Content).Name;
            var doc = new Document(path);

            var wnd = Application.Current.MainWindow as MainWindow;
            wnd.DocumentsManager.Documents.Add(doc);
            wnd.DocumentsManager.SelectLastDocument();
            
            NavigationCommands.GoToPage.Execute("/Pages/Workspace.xaml", null);
        }
    }
}
