using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chartful.Model
{
    public class Manager
    {
        public ObservableCollection<Document> Documents { get; private set; }

        public Manager()
        {
            Documents = new ObservableCollection<Document>();
        }

        public void SelectDocument(string name)
        {
            foreach (Document d in Documents)
                if (d.Name == name)
                    d.IsSelected = true;
                else
                    d.IsSelected = false;
        }

        public void SelectLastDocument()
        {
            var d = Documents.Last<Document>();
            SelectDocument(d.Name);
        }

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
    }
}
