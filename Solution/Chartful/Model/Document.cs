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
        // for wpf binding
        public event PropertyChangedEventHandler PropertyChanged;

        string path;
        string name;
        bool isSelected;
        
        public List<UIObject> Content { get; set; }

        /// <summary>
        /// New document
        /// </summary>
        /// <param name="p">document's path, defaut value is authorized</param>
        public Document(string p = "New Document.ctf")
        {
            Content = new List<UIObject>();

            Path = p;
            Name = path;

        }

        /// <summary>
        /// Get a representation string of the document
        /// </summary>
        /// <returns>Formated name and path</returns>
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

            //Set name with a splited path 
            private set
            {
                name = value.Split('\\')[value.Split('\\').Length - 1];
                //For WPF binding
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
                //For WPF binding
                RaisePropertyChanged("IsSelected");
            }
        }

        public string Path
        {
            get
            {
                return path;
            }

            // Set every document's UIObject when the path is set
            set
            {
                path = value;
                ParseFromXML();
            }
        }

        /// <summary>
        /// return BBCode link
        /// </summary>
        public string BBCode
        {
            get
            {
                return string.Format("[url={0}][b]{1}[/b] - {0}[/url]", path, name);
            }
        }

        /// <summary>
        /// Notify any property's changed for WPF binding
        /// </summary>
        /// <param name="propertyName">Name of the property</param>
        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged == null) return;
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Write the document in a .ctf files
        /// </summary>
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

        /// <summary>
        /// read and parse every file's Object
        /// </summary>
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
