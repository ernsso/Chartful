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

            Selected = mainWindow.DocumentsManager.Selected;

            if (!string.IsNullOrEmpty(this.Selected.Name))
                this.DocumentName.Text = this.Selected.Name;
        }
        #endregion

        private void TextContent_Changed(object sender, TextChangedEventArgs e)
        {
            this.Selected.Update(sender as TextBox);

            //var value = GetModification(sender as TextBox);
            //this.Selected.Update(value);
            if (null != this.Selected.Name)
            {
                var document = this.Selected;
                var data = new Data()
                    {
                        DocumentID = document.Name,
                        PropertyName = "Text",
                        Value = this.TextContent.Text
                    };

                mainWindow.MyPeerChannel.SendData(data);
            }
        }

        /// <summary>
        /// Get the diffrence between the TextBox and the document 
        /// </summary>
        /// <returns></returns>
        private string GetModification(TextBox textBox)
        {
            return null;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            string path = null;

            if (null == Selected.Path)
                path = mainWindow.DocumentsManager.SelectPath();

            if (null != path)
                this.Selected.Path = path;

            if (null != this.Selected.Path)
                Selected.ParseToXML();
        }

        private void Share_Click(object sender, RoutedEventArgs e)
        {
            if ("SHARE" == (string)this.Share.Content && !string.IsNullOrEmpty(this.DocumentName.Text))
            {
                this.Selected.Name = this.DocumentName.Text;
                this.Share.Content = "UNSHARE";
            }
            else
            {
                this.Selected.Name = "";
                this.Share.Content = "SHARE";
            }
        }
    }
}
