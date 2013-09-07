using Chartful.Model;
using Chartful.Pages;
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

namespace Chartful.Controls
{
    /// <summary>
    /// Logique d'interaction pour MenuList.xaml
    /// </summary>
    public partial class MenuList : UserControl
    {
        public MenuList()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ListItemsProperty = DependencyProperty.Register("ListItems",
            typeof(ObservableCollection<Document>),
            typeof(MenuList),
            null);

        public ObservableCollection<Document> ListItems
        {
            get { return (ObservableCollection<Document>)GetValue(ListItemsProperty); }
            set { SetValue(ListItemsProperty, value); }
        }

        private void TextItemList_MouseClick(object sender, MouseButtonEventArgs e)
        {
            var wnd = Application.Current.MainWindow as MainWindow;
            wnd.DocumentsManager.SelectDocument(((TextItemList)sender).TextItem);
        }
    }
}
