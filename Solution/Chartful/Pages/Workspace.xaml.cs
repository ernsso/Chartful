using Chartful.Controls;
using Chartful.Model;
using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
        //Point canvasCursorLocation;

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
            if (null != selected)
                controls.IsEnabled = true;

            var wnd = Application.Current.MainWindow as MainWindow;
            Documents = wnd.DocManager.Documents;
            Selected = wnd.DocManager.Selected;

            SetUIObject();

            DataContext = null;
            DataContext = this;
        }

        public void GetUIObject()
        {
            if (null != Selected)
            {
                Selected.Content.Clear();

                foreach (UIElement e in dragCanvas.Children)
                {
                    UIObject o = new UIObject();

                    o.Text = ((TextBlock)e).Text;
                    o.FontSize = ((TextBlock)e).FontSize;
                    o.Left = Canvas.GetLeft(e);
                    o.Top = Canvas.GetTop(e);

                    Selected.Content.Add(o);
                }
            }
        }

        public void SetUIObject()
        {
            if (null != Selected)
            {
                dragCanvas.Children.Clear();

                foreach (UIObject o in Selected.Content)
                {
                    TextBlock item = new TextBlock { Text = "Your Title" };
                    item.FontSize = 36;
                    item.FontWeight = FontWeights.Bold;
                    item.Background = Brushes.Transparent;

                    dragCanvas.Children.Add(item);
                    Canvas.SetLeft(item, o.Left);
                    Canvas.SetTop(item, o.Top);
                }
            }
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
                obj.SetData(typeof(WrapPanel), source.Content);
                effects = DragDrop.DoDragDrop(source, obj, DragDropEffects.Copy | DragDropEffects.Move);
            }
        }

        //private void canvas_MouseMove(object sender, MouseEventArgs e)
        //{
        //    canvasCursorLocation = e.GetPosition(this);
        //}

        private void receiver_Drop(object sender, DragEventArgs e)
        {
            if (Selected == null)
            {
                ModernDialog.ShowMessage("before editing, you should create or open a document", "No document", MessageBoxButton.OK);
                return;
            }

            if (e.Data.GetDataPresent(typeof(WrapPanel)))
            {
                e.Effects = DragDropEffects.Copy;
                string typeName = ((TextBlock)((WrapPanel)e.Data.GetData(typeof(WrapPanel))).Children[1]).Text;

                if (typeName == "Title")
                {
                    Point relativePoint = dragCanvas.TransformToAncestor(this)
                                  .Transform(new Point(0, 0));

                    UIObject o = new UIObject();

                    o.Left = e.GetPosition(this).X - relativePoint.X;
                    o.Top = e.GetPosition(this).Y - relativePoint.Y;

                    Selected.Content.Add(o);
                }

                this.StatusContent.Children.Add(new TextBlock { Text = "Add : " + typeName });
                Refresh();
            }
            else
                e.Effects = DragDropEffects.None;
        }

        private void dragCanvas_MouseLeave(object sender, MouseEventArgs e)
        {
            GetUIObject();
        }
    }
}
