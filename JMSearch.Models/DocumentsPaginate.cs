using System;
using System.Collections.Generic;
using System.Text;

namespace JMSearch.Models
{
    public class DocumentsPaginate
    {
        public List<Document> Documents { get; set; }

        public long MaxPages { get; set; }

        public DocumentsPaginate()
        {
            Documents = new List<Document>();
            MaxPages = 1;
        }
    }
}
