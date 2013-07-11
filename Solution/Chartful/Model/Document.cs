using Chartful.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Xml;

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
            Content = new List<UIObject>();

            Path = p;
            Name = path;

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

            set
            {
                path = value;
                ParseFromXML();
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

        public void ParseToXML()
        {
            try
            {
                XmlTextWriter myXmlTextWriter = new XmlTextWriter(this.path, System.Text.Encoding.UTF8);
                myXmlTextWriter.Formatting = Formatting.Indented;
                myXmlTextWriter.WriteStartDocument(false);

                myXmlTextWriter.WriteStartElement("Objects", null);
                foreach (UIObject o in Content)
                {
                    // écrire dans le fichier 
                    myXmlTextWriter.WriteStartElement("Object", null);
                        myXmlTextWriter.WriteAttributeString("Text", o.Text);
                        myXmlTextWriter.WriteAttributeString("FontSize", o.FontSize.ToString());
                        myXmlTextWriter.WriteAttributeString("Top", o.Top.ToString());
                        myXmlTextWriter.WriteAttributeString("Text", o.Left.ToString());                
                    myXmlTextWriter.WriteEndElement();
                }
                myXmlTextWriter.WriteEndElement();
                myXmlTextWriter.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }


        }

        public void ParseFromXML()
        {
            try
            {
                XDocument doc = XDocument.Load(this.path);
                string _text;
                string _fontsize;
                string _top;
                string _left;

                foreach (var obj in doc.Descendants("Object"))
                {
                    _text = obj.Attribute("Text").Value;
                    _fontsize = obj.Attribute("FontSize").Value;
                    _top = obj.Attribute("Top").Value;
                    _left = obj.Attribute("Left").Value;

                    UIObject cxml = new UIObject(_text, _fontsize, _top, _left);
                    Content.Add(cxml);
                }

                foreach (UIObject ui in Content)
                {
                    Console.WriteLine(ui.Text);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }

}
