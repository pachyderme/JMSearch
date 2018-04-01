using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMSearch.Models
{
    public class Document
    {
        #region Properties
        /// <summary>
        /// Document identifier
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Name of the document
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// ParagraphContent of the document
        /// </summary>
        public string Paragraph { get; set; }
        /// <summary>
        /// Number of view in for one paragraph
        /// Sum of them specifiy the most read docuement
        /// </summary>
        public int ViewNumber { get; set; }
        #endregion
    }
}
