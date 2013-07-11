using Chartful.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Chartful.Model
{
    public class Document : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        string path;
        string name;
        bool isSelected;
        
        public List<UIObject> Content { get; set; }

        public Document(string p = "New Document.ctf")
        {
            path = p;
            Name = path;

            Content = new List<UIObject>();
        }

        public override string ToString()
        {
            return string.Format("{0} - {1}", name, path);
        }

        public string Name
        {
            get
            {
                return name;
            }

            private set
            {
                name = value.Split('\\')[value.Split('\\').Length - 1];
                RaisePropertyChanged("Name");
            }
        }

        public bool IsSelected
        {
            get
            {
                return isSelected;
            }

            set
            {
                isSelected = value;
                RaisePropertyChanged("IsSelected");
            }
        }

        public string Path
        {
            get
            {
                return path;
            }
        }

        public string BBCode
        {
            get
            {
                return string.Format("[url=/Pages/Workspace.xaml][b]{1}[/b] - {0}[/url]", path, name);
            }
        }

        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged == null) return;
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
