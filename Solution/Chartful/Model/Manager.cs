﻿using Chartful.BLL;
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

        #region Constructors
        public Manager()
        {
            Documents = new ObservableCollection<Document>();
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
                if (d.Name == name)
                    d.IsSelected = true;
                else
                    d.IsSelected = false;

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

                return null;
            }
        }

        public void Set(Data data)
        {
            var document = Find(data.DocumentID);

            if(null != document)
                document.Update(data);
        }

        public WebDocument Find(string documentId)
        {
            foreach (WebDocument d in Documents)
                if (d.Id == documentId)
                    return d;

            return null;
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

        public bool OpenFile(string webFileName)
        {
            Documents.Add(new WebDocument(webFileName));
            
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
        #endregion
    }
}
