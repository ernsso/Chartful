using Chartful.BLL;
using Chartful.BLL.p2p;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chartful.Model
{
    public class Manager
    {
        public ObservableCollection<Document> Documents { get; private set; }
        public List<string> SharedDocumentsNames { get; set; }

        public string UserId { get; private set; }

        #region Constructors
        public Manager()
        {
            this.Documents = new ObservableCollection<Document>();
            this.SharedDocumentsNames = new List<string>();

            this.UserId = DateTime.Now.ToString("HHmmssfff");
        }
        #endregion

        #region Document Management
        /// <summary>
        /// Set to Selected the Document 
        /// </summary>
        /// <param name="name">Document's name</param>
        public void SelectDocument(string name)
        {
            foreach (Document d in Documents)
                d.IsSelected = false;

            Documents.Last<Document>().IsSelected = true;
        }

        /// <summary>
        /// Set to Selected the last Document
        /// </summary>
        public void SelectLastDocument()
        {
            var d = Documents.Last<Document>();
            SelectDocument(d.Name);
        }

        /// <summary>
        /// return the Selected Document
        /// </summary>
        public Document Selected
        {
            get
            {
                foreach (Document d in Documents)
                    if (d.IsSelected)
                        return d;

                this.Documents.Add(new Document());
                SelectLastDocument();

                return this.Selected;
            }
        }

        public void Set(Data data)
        {
            var document = Find(data.DocumentName);

            if (null != document)
            {
                document.Update(data);
            }
        }

        public Document Find(string documentName)
        {
            foreach (Document d in Documents)
                if (d.Name == documentName)
                    return d;

            return null;
        }

        public void AddDocumentsName(string name)
        {
            if (!SharedDocumentsNames.Contains(name))
                SharedDocumentsNames.Add(name);
        }
        #endregion

        #region File Management
        /// <summary>
        /// Open an existing file
        /// </summary>
        /// <returns>true if the file is opened or false if cancelled</returns>
        public bool OpenFile()
        {
            var ofd = new OpenFileDialog();
            ofd.InitialDirectory = "c:\\";
            ofd.Filter = "Chartful files (*.ctf)|*.ctf|All files (*.*)|*.*";

            Nullable<bool> result = ofd.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                //Add the opened file to the List of Documents
                Documents.Add(new Document(ofd.FileName));
                SelectLastDocument();
            }

            return result == true ? true : false;
        }

        public bool OpenFile(string fileName)
        {
            this.Documents.Remove(Find(fileName));

            this.Documents.Add(new Document());
            this.Documents.Last().Name = fileName;
            this.SelectLastDocument();

            return true;
        }

        /// <summary>
        /// create a new file
        /// </summary>
        /// <returns>true if the file is created or false if cancelled</returns>
        public bool NewFile()
        {
            var sfd = new SaveFileDialog();
            sfd.InitialDirectory = "c:\\";
            sfd.Filter = "Chartful files (*.ctf)|*.ctf|All files (*.*)|*.*";
            sfd.FileName = "New Document"; // Default file name
            sfd.DefaultExt = ".ctf";

            Nullable<bool> result = sfd.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                File.Create(sfd.FileName);

                //Add the selected file to the List of Documents
                Documents.Add(new Document(sfd.FileName));
                SelectLastDocument();
            }

            return result == true ? true : false;
        }

        public string SelectPath()
        {            
            var sfd = new SaveFileDialog();
            sfd.InitialDirectory = "c:\\";
            sfd.Filter = "Chartful files (*.ctf)|*.ctf|All files (*.*)|*.*";
            sfd.FileName = "New Document"; // Default file name
            sfd.DefaultExt = ".ctf";

            Nullable<bool> result = sfd.ShowDialog();

            // Process open file dialog box results
            if (result == true)
                return sfd.FileName;

            return null;
        }

        public bool SetName(Document document, string name)
        {
            if (this.SharedDocumentsNames.Contains(name))
                return false;

            document.Name = name;
            return true;
        }
        #endregion
    }
}
