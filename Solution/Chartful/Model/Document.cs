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
using Chartful.BLL;
using System.Windows;
using Chartful.BLL.p2p;

namespace Chartful.Model
{
    public class Document : INotifyPropertyChanged
    {
        // for wpf binding
        public event PropertyChangedEventHandler PropertyChanged;

        public string Name { get; set; }
        public int Caret { get; set; }

        string path;
        string text = "";

        bool isSelected;

        public List<UIObject> Objects { get; set; }
        List<Data> logs;

        public int LastObjectId { get; set; }
        public int FocusedObjectId { get; set; }
        
        #region Constructors
        /// <summary>
        /// New document
        /// </summary>
        /// <param name="p">document's path, defaut value is authorized</param>
        public Document()
        {
            Objects = new List<UIObject>();
            logs = new List<Data>();

            LastObjectId = 0;
            FocusedObjectId = -1;
            isSelected = false;

            Caret = 0;
        }

        public Document(string path)
            : base()
        {
            Path = path;
        }
        #endregion

        #region Accessors

        public string Path
        {
            get
            {
                return this.path;
            }

            set
            {
                this.path = value;
                //Set every document's UIObject when the path is set
                ParseFromXML();
            }
        }

        public string ShortPath
        {
            get
            {
                if (null != path)
                   return this.path.Split('\\')[this.path.Split('\\').Length - 1]; 
                
                return null;
            }
        }

        public string Text
        {
            get
            {
                return text;
            }

            set
            {
                text = value;
                //For WPF binding
                RaisePropertyChanged("Text");
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

        /// <summary>
        /// return BBCode link
        /// </summary>
        public string BBCode
        {
            get
            {
                return string.Format("[url={0}][b]{1}[/b] - {0}[/url]", path, Name);
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
        #endregion

        #region Parsors
        /// <summary>
        /// Get a representation string of the document
        /// </summary>
        /// <returns>Formated name and path</returns>
        public override string ToString()
        {
            return string.Format("{0} - {1}", Name, path);
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

                myXmlTextWriter.WriteStartElement("Text", null);
                    myXmlTextWriter.WriteAttributeString("Content", this.text);
                myXmlTextWriter.WriteEndElement();

                //foreach (UIObject o in Objects)
                //{
                //    // écrire dans le fichier 
                //    myXmlTextWriter.WriteStartElement("Object", null);
                //        myXmlTextWriter.WriteAttributeString("Id", o.ID);
                //        myXmlTextWriter.WriteAttributeString("Text", o.Content);
                //        myXmlTextWriter.WriteAttributeString("Type", o.UIType);
                //        myXmlTextWriter.WriteAttributeString("FontSize", o.FontSize.ToString());
                //        myXmlTextWriter.WriteAttributeString("Top", o.Top.ToString());
                //        myXmlTextWriter.WriteAttributeString("Left", o.Left.ToString());                
                //    myXmlTextWriter.WriteEndElement();
                //}
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
                foreach (var obj in doc.Descendants("Text"))
                {
                    this.Text = obj.Attribute("Content").Value;
                }

                //string _id;
                //string _text;
                //string _type;
                //string _fontsize;
                //string _top;
                //string _left;

                //foreach (var obj in doc.Descendants("Object"))
                //{
                //    _id = obj.Attribute("Id").Value;
                //    _text = obj.Attribute("Text").Value;
                //    _type = obj.Attribute("Type").Value;
                //    _fontsize = obj.Attribute("FontSize").Value;
                //    _top = obj.Attribute("Top").Value;
                //    _left = obj.Attribute("Left").Value;

                //    UIObject cxml = new UIObject(_id, _text,_type, _fontsize, _top, _left);
                //    Objects.Add(cxml);

                //    LastObjectId++;
                //}
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
                foreach (UIObject o in Objects)
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
        #endregion

        #region Content Management
        public void Update(TextBox textBox)
        {
            this.Text = textBox.Text;
        }

        public void Update(Data data)
        {
            if (this.isSelected && !String.IsNullOrEmpty(data.Value))
            {
                //Test
                int i;
                string diff = data.Value;

                //Determine a quel caractere on peut determiner si l'on doit ajouter ou supprimer des caractères
                for (i = 0; i < diff.Length; i++)
                {
                    if (diff[i].ToString() == "+" || diff[i].ToString() == "-")
                        break;
                }

                //Prend le nombre de caractères avant d'effecter l'opération
                string[] modif = diff.Split(new Char[] { '+', '-' });
                int nbcar = -1;
                nbcar = int.Parse(modif[0]);

                string newtext = "";
                for (int c = i + 1; c < diff.Length; c++)
                {
                    newtext = newtext + diff[c].ToString();
                }

                //Défini en fonction de l'opérateur le texte a retourner
                if (nbcar >= 0)
                {
                    if (diff[i].ToString() == "+")
                    {
                        this.Text = this.Text.Insert(nbcar, newtext);
                        this.Caret += nbcar;
                    }
                    else
                    {
                        this.Text = this.Text.Remove(nbcar - int.Parse(modif[1]), int.Parse(modif[1]));
                        this.Caret -= nbcar;
                    }
                }
                logs.Add(data);
            }
            else
            {
                this.text = data.Value;
            }
        }
        #endregion
    }
}
