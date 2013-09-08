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

namespace Chartful.Controls
{
    /// <summary>
    /// Logique d'interaction pour TextItemList.xaml
    /// </summary>
    public partial class TextItemList : UserControl
    {
        public TextItemList()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register("IsSelected",
            typeof(bool),
            typeof(TextItemList),
            null);

        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        public static readonly DependencyProperty textItemProperty = DependencyProperty.Register("TextItem",
            typeof(string),
            typeof(TextItemList),
            null);

        public string TextItem
        {
            get { return (string)GetValue(textItemProperty); }
            set { SetValue(textItemProperty, value); }
        }
    }
}
