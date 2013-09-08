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
using Chartful.Net;
using System.Windows;

namespace Chartful.Model
{
    public class Document : INotifyPropertyChanged
    {
        // for wpf binding
        public event PropertyChangedEventHandler PropertyChanged;

        string path;
        string name;
        bool isSelected;

        public int Focused { get; set; }
        public List<UIObject> Content { get; set; }
        public int LastID { get; set; }

        /// <summary>
        /// New document
        /// </summary>
        /// <param name="p">document's path, defaut value is authorized</param>
        public Document(string p = "New Document.ctf")
        {
            Content = new List<UIObject>();

            Path = p;
            Name = path;
            LastID = 0;
            Focused = -1;
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
        {/*
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
                        myXmlTextWriter.WriteAttributeString("Id", o.ID);
                        myXmlTextWriter.WriteAttributeString("Text", o.Content);
                        myXmlTextWriter.WriteAttributeString("Type", o.UIType);
                        myXmlTextWriter.WriteAttributeString("FontSize", o.FontSize.ToString());
                        myXmlTextWriter.WriteAttributeString("Top", o.Top.ToString());
                        myXmlTextWriter.WriteAttributeString("Left", o.Left.ToString());                
                    myXmlTextWriter.WriteEndElement();
                }
                myXmlTextWriter.WriteEndElement();
                myXmlTextWriter.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }*/


        }

        /// <summary>
        /// read and parse every file's Object
        /// </summary>
        public void ParseFromXML()
        {
            try
            {
                XDocument doc = XDocument.Load(this.path);
                string _id;
                string _text;
                string _type;
                string _fontsize;
                string _top;
                string _left;

                foreach (var obj in doc.Descendants("Object"))
                {
                    _id = obj.Attribute("Id").Value;
                    _text = obj.Attribute("Text").Value;
                    _type = obj.Attribute("Type").Value;
                    _fontsize = obj.Attribute("FontSize").Value;
                    _top = obj.Attribute("Top").Value;
                    _left = obj.Attribute("Left").Value;

                    UIObject cxml = new UIObject(_id, _text,_type, _fontsize, _top, _left);
                    Content.Add(cxml);

                    LastID++;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        /// <summary>
        /// Parse every document's object into HTML
        /// </summary>
        /// <returns>HTML objects</returns>
        public string ParseToHTML()
        {
            string html_content = "<html><header></header><body>";
            html_content = html_content + "<div style=”Width:596px; Height : 896px; Margin:0; Padding:0; Background:#ffffff; border:solid #999999 1px;font-family:arial; font-size:12px; color:#333333””>";
            try
            {
                foreach (UIObject o in Content)
                {
                    if (o.UIType == "Image")
                    {
                        html_content = html_content + "<IMG style=”position:absolute; Top:" + o.Top + "px; left:" + o.Left + "px;” src=”"+o.Content+"” />";
                    }
                    else
                    {
                        html_content = html_content + "<span style=”position:absolute; Top:"+o.Top+"px; left:"+o.Left+"px;”>"+o.Content+"</span>";

                    }

                }

                html_content = html_content + "</div></body></html>";

                return html_content;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return "";
            }
        }

        /// <summary>
        /// Update the object in the document
        /// </summary>
        /// <param name="o"></param>
        public void UpdateUIObject(UIObject o)
        {
            int i = FindUIObject(o.ID);

            if (-1 < i)
            {
                Content[i] = o;

                //Rewrite the file
                ParseToXML();
                RaisePropertyChanged("Name");
            }
            else
            {
                Content.Add(o);
            }
        }

        /// <summary>
        /// Find an ObjetUI's position  
        /// </summary>
        /// <param name="id">ObjectUI's ID</param>
        /// <returns>ObjectUI's index or -1 if not found</returns>
        public int FindUIObject(string id)
        {
            for (int i = 0; i < Content.Count; i++)
                if (Content[i].ID == id) return i;
            return -1;
        }
    }
}
