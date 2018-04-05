using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMSearch.Models
{
    public class History
    {
        #region Properties

        /// <summary>
        /// Id of the history
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// User Id of the history
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// DocumentId of the history
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string DocumentId { get; set; }

        /// <summary>
        /// Keyword of the history
        /// </summary>
        public string KeyWord { get; set; }

        #endregion
    }
}
