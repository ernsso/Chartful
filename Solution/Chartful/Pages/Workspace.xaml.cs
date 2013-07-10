using Chartful.Controls;
using Chartful.Model;
using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;
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
    /// Interaction logic for Workspace.xaml
    /// </summary>
    public partial class Workspace : UserControl
    {
        public ObservableCollection<Document> Documents { get; private set; }
        Document selected;

        public Document Selected
        {
            get
            {
                return selected;
            }
            set
            {
                selected = value;
            }
        }

        public Workspace()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        public void Refresh()
        {
            var wnd = Application.Current.MainWindow as MainWindow;
            Documents = wnd.DocManager.Documents;
            Selected = wnd.DocManager.Selected;

            DataContext = null;
            DataContext = this;
        }
        
        private void TextItemList_MouseClick(object sender, MouseButtonEventArgs e)
        {
            var wnd = Application.Current.MainWindow as MainWindow;
            wnd.DocManager.SelectDocument(((TextItemList)sender).TextItem);
            Refresh();
        }

        // Drag and Drop
        private void pickData_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragDropEffects effects;
                DataObject obj = new DataObject();
                Label source = (Label)sender;
                obj.SetData(typeof(string), source.Content);
                effects = DragDrop.DoDragDrop(source, obj, DragDropEffects.Copy | DragDropEffects.Move);
            }
        }

        private void receiver_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(string)))
            {
                e.Effects = DragDropEffects.Copy;
                string weapon = (string)e.Data.GetData(typeof(string));
                this.receiver.Children.Add(new TextBlock { Text = weapon });

                this.StatusContent.Children.Add(new TextBlock { Text = "Add : " + weapon });
            }
            else
                e.Effects = DragDropEffects.None;
        }
    }
}
