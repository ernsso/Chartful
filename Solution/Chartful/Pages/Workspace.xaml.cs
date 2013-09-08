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
            this.mainWindow.MyPeerChannel.SendString("-get docNames");
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void Refresh()
        {
            this.Selected = mainWindow.DocumentsManager.Selected;

            if (!string.IsNullOrEmpty(this.Selected.Name))
            {
                this.DocumentName.Text = this.Selected.Name;
                this.mainWindow.MyPeerChannel.SendString(string.Format("-get {0}", this.Selected.Name));
            }

            DataContext = null;
            DataContext = this;

            this.Selected = mainWindow.DocumentsManager.Selected;
            this.Selected.Caret = 0;
        }
        #endregion

        #region Events
        
        private void TextContent_Changed(object sender, TextChangedEventArgs e)
        {
        }
        
        private void TextContent_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
        }

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

        private void TextContent_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void TextContent_KeyUp(object sender, KeyEventArgs e)
        {
            int nbcar = -1;
            //on fait un update
            //this.selected.update(sender as textbox);

            if (null != this.Selected.Name)
            {
                var document = this.Selected;

                //diff entre l'ancien et le nouveau text
                var diff = Diff(textOld, this.TextContent.Text);

                var data = new Data()
                {
                    Id = DateTime.Now.ToString("hhmmssfff"),
                    DocumentName = document.Name,
                    UserId = this.mainWindow.DocumentsManager.UserId,
                    PropertyName = "text",
                    Value = diff
                };

                mainWindow.MyPeerChannel.SendData(data);

                string[] modif = diff.Split(new char[] { '+', '-' });
                if (nbcar > -1)
                    nbcar = int.Parse(modif[0]);
                
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

    }
}
