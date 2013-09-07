using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chartful.Model
{
    public class WebDocument : Document
    {
        public string Id { get; set; }

        public WebDocument(string id)
            : base()
        {
            this.Id = id;
        }

        public void Update(Data data)
        {
        }
    }
}
