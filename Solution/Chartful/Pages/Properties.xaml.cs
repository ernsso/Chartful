using Chartful.Controls;
using Chartful.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for Properties.xaml
    /// </summary>
    public partial class Properties : UserControl
    {
        public ObservableCollection<Document> Documents { get; private set; }
        public Document Selected { get; set; }

        public Properties()
        {
            InitializeComponent();
            DataContext = this;
        }


        /// <summary>
        /// Called when the page is loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        /// <summary>
        /// Refresh the editor
        /// </summary>
        public void Refresh()
        {
            var wnd = Application.Current.MainWindow as MainWindow;

            // Get all opened documents
            Documents = wnd.DocManager.Documents;

            // Get selected document
            Selected = wnd.DocManager.Selected;

            // Refresh Data Context
            DataContext = null;
            DataContext = this;
        }

        /// <summary>
        /// Change the selected document
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextItemList_MouseClick(object sender, MouseButtonEventArgs e)
        {
            var wnd = Application.Current.MainWindow as MainWindow;
            wnd.DocManager.SelectDocument(((TextItemList)sender).TextItem);
            Refresh();
        }
    }
}
