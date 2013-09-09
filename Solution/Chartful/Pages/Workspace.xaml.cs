using Chartful.BLL;
using Chartful.BLL.p2p;
using Chartful.Controls;
using Chartful.Model;
using FirstFloor.ModernUI.Windows.Controls;
using System;
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

        string textOld = "";

        #region Constructors
        public Workspace()
        {
            InitializeComponent();
            DataContext = this;

            this.mainWindow = Application.Current.MainWindow as MainWindow;
            this.mainWindow.Editor = this;
            this.mainWindow.MyPeerChannel.SendString("-get docNames");
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        public void Refresh()
        {
            this.Selected = mainWindow.DocumentsManager.Selected;

            if (this.UIObjectList.SelectedItem != null)
                this.Selected.CurrentObject = ((UIObject)this.UIObjectList.SelectedItem);

            if (!string.IsNullOrEmpty(this.Selected.Name))
            {
                this.DocumentName.Text = this.Selected.Name;
                this.mainWindow.MyPeerChannel.SendString(string.Format("-get {0}", this.Selected.Name));
            }

            this.dragCanvas.Children.Clear();

            foreach (var obj in this.Selected.Objects)
            {
                var element = obj.ToUIElement();
                dragCanvas.Children.Add(element);
                Canvas.SetLeft(element, obj.Left);
                Canvas.SetTop(element, obj.Top);
            }

            DataContext = null;
            DataContext = this;

            this.Selected = mainWindow.DocumentsManager.Selected;
            this.Selected.Caret = 0;

            if (null != this.Selected.CurrentObject)
                this.UIObjectList.SelectedItem = this.Selected.CurrentObject;
        }
        #endregion

        #region Events
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            string path = null;

            if (null == Selected.Path)
                path = mainWindow.DocumentsManager.SelectPath();

            if (null != path)
                this.Selected.Path = path;

            if (null != this.Selected.Path)
            {
                Selected.ParseToXML();

                new ModernDialog
                {
                    Title = "SAVE",
                    Content = "Votre document est enregistré"
                }.ShowDialog();
            }
        }

        private void Share_Click(object sender, RoutedEventArgs e)
        {
            this.Share.IsEnabled = false;
            var newName = this.DocumentName.Text.Replace(" ", "");

            if ("SHARE" == (string)this.Share.Content && !string.IsNullOrEmpty(newName))
            {
                this.mainWindow.MyPeerChannel.SendString(string.Format("-del {0}", this.Selected.Name));

                var hasName = this.mainWindow.DocumentsManager.SetName(this.Selected, newName);
                //this.Selected.Name = this.DocumentName.Text;

                if (!hasName)
                    new ModernDialog
                    {
                        Title = "SHARE",
                        Content = "Un document avec le même nom existe déjà.\nChoisissez un nom unique ou connectez vous au document."
                    }.ShowDialog();
                else
                {
                    this.mainWindow.MyPeerChannel.SendString(string.Format("-add {0}", newName));
                    this.Share.Content = "UNSHARE";
                }
            }
            else
            {
                this.mainWindow.MyPeerChannel.SendString(string.Format("-del {0}", this.Selected.Name));
                this.Selected.Name = "";
                this.DocumentName.Text = "";
                this.Share.Content = "SHARE";
            }

            this.Share.IsEnabled = true;
        }

        private void TextContent_KeyUp(object sender, KeyEventArgs e)
        {
            SetContent();
            (this.UIObjectList.SelectedItem as UIObject).Content = this.TextContent.Text;

            if (null != this.Selected.Name)
            {
                var document = this.Selected;

                //diff entre l'ancien et le nouveau text
                var diff = Diff(textOld, this.TextContent.Text);

                var data = new Data()
                {
                    Id = DateTime.Now.ToString("hhmmssfff"),
                    DocumentName = document.Name,
                    ObjectName = (this.UIObjectList.SelectedItem as UIObject).Name,
                    UserId = this.mainWindow.DocumentsManager.UserId,
                    PropertyName = "Text",
                    Value = diff
                };
                mainWindow.MyPeerChannel.SendData(data);
            }

            //on enregistre les modifications   
            this.textOld = this.TextContent.Text;
        }
        #endregion

        string Diff(string first, string second)
        {
            string min = first;
            string max = second;
            if (second.Length < first.Length)
            {
                min = second;
                max = first;
            }


            int c = 1;
            int d = 1;
            d = max.Length - min.Length;


            if (first.Length == 0)
            {
                return "0+" + max;
            }
            else
            {
                for (int i = 0; i < min.Length; i++)
                {
                    char _first = first[i];
                    char _second = second[i];


                    if (_second != _first)
                    {
                        if (second.Length > first.Length)
                        {
                            string ca = "";
                            for (int e = 0; e < d; e++)
                                ca = ca + second[i + e];
                            return i.ToString() + "+" + ca;
                        }
                        else
                        {
                            i = i + d;
                            return i.ToString() + "-" + d;
                        }
                    }
                    c = i + 1;
                }
                if (second.Equals(max))
                {
                    string ca = "";
                    for (int e = 0; e < d; e++)
                        ca = ca + second[c + e];
                    return c.ToString() + "+" + ca;
                }
                else
                    return max.Length.ToString() + "-" + d;
            }
        }


        #region Drag & Drop
        /// <summary>
        /// Get the dragged Element
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Drop the dragged Element
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    //Get the Canvas'position
                    Point relativePoint = dragCanvas.TransformToAncestor(this)
                                  .Transform(new Point(0, 0));

                    UIObject obj = new UIObject { Content = "New Title" };
                    obj.Name = string.Format("Title{0}", DateTime.Now.ToString("yyyyMMddHHmmss"));
                    obj.FontSize = 36;
                    obj.Top = e.GetPosition(this).Y - relativePoint.Y;
                    obj.Left = e.GetPosition(this).X - relativePoint.X;

                    var element = obj.ToUIElement();
                    dragCanvas.Children.Add(element);
                    Canvas.SetLeft(element, obj.Left);
                    Canvas.SetTop(element, obj.Top);

                    this.Selected.Objects.Add(obj);

                    if (null != this.Selected.Name)
                    {
                        var data = new Data()
                        {
                            Id = DateTime.Now.ToString("hhmmssfff"),
                            DocumentName = this.Selected.Name,
                            ObjectName = obj.Name,
                            UserId = this.mainWindow.DocumentsManager.UserId,
                            PropertyName = "Create",
                            Value = string.Format("{0};{1}", obj.Left, obj.Top)
                        };
                        this.mainWindow.MyPeerChannel.SendData(data);
                    }
                }
            }
            else
                e.Effects = DragDropEffects.None;
        }
        #endregion

        private void UIObjectList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (null != this.UIObjectList.SelectedItem)
            {
                this.TextContent.Text = (this.UIObjectList.SelectedItem as UIObject).Content;
                this.Selected.Text = (this.UIObjectList.SelectedItem as UIObject).Content;
                this.textOld = this.TextContent.Text;
            }
        }

        private void SetContent()
        {
            foreach (TextBlock element in this.dragCanvas.Children)
                if (null != this.UIObjectList.SelectedItem
                    && element.Name == (this.UIObjectList.SelectedItem as UIObject).Name)
                {
                    element.Text = this.TextContent.Text;
                    this.Selected.Text = this.TextContent.Text;
                }
        }

        private void FontSize_LostFocus(object sender, RoutedEventArgs e)
        {
            int size = 0;

            if (!string.IsNullOrEmpty(this.FontSizeContent.Text)
                && int.TryParse(this.FontSizeContent.Text, out size)
                && null != this.UIObjectList.SelectedItem)
            {
                var obj = (this.UIObjectList.SelectedItem as UIObject);

                foreach (TextBlock element in this.dragCanvas.Children)
                    if (element.Name == (obj.Name))
                    {
                        element.FontSize = size;
                        obj.FontSize = size;
                    }

                if (null != this.Selected.Name)
                {
                    var data = new Data()
                    {
                        Id = DateTime.Now.ToString("hhmmssfff"),
                        DocumentName = this.Selected.Name,
                        ObjectName = obj.Name,
                        UserId = this.mainWindow.DocumentsManager.UserId,
                        PropertyName = "Size",
                        Value = string.Format("{0}", size)
                    };
                    this.mainWindow.MyPeerChannel.SendData(data);
                }
            }
        }
    }
}
