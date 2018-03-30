using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMSearch.Models
{
    public class Document
    {
        #region Proprietes
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

        #region Contructors
        /// <summary>
        /// Document Constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="paragraph"></param>
        /// <param name="viewNumber"></param>
        public Document(int id, string name, string paragraph, int viewNumber)
        {
            Id = id;
            Name = name;
            Paragraph = paragraph;
            ViewNumber = viewNumber;
        }
        #endregion
    }
}
