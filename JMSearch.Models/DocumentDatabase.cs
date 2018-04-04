using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMSearch.Models
{
    public class DocumentDatabase
    {
        #region Field
        /// <summary>
        /// MongoClient for database connexion 
        /// </summary>
        private MongoClient _Client;
        /// <summary>
        /// MongoServer for database connexion
        /// </summary>
        private MongoServer _Server;
        /// <summary>
        /// Database Mongo
        /// </summary>
        private MongoDatabase _Db;
        private int _Id;
        #endregion

        #region Constructor
        public DocumentDatabase()
        {
            _Client = new MongoClient("mongodb://192.168.206.125:27017");
            _Server = _Client.GetServer();
            _Db = _Server.GetDatabase("Document");
        }
        #endregion

        #region Methods
        /// <summary>
        /// Document creation and database saving
        /// </summary>
        /// <param name="document"></param>
        public void Create(Document document)
        {
            document.Id = _Id++;
            _Db.GetCollection<Document>("Document").Save(document);
        }

        /// <summary>
        /// Get the documents by page
        /// </summary>
        /// <param name="document"></param>
        public List<Document> GetDocumentByPage(string keyWord, int lastPage)
        {
            var context = _Db.GetCollection<Document>("Document");

            var filterDocumentBuilder = Builders<Document>.Filter;

            return GetListPaginate(context, keyWord, lastPage, Query<Document>.Where(d => d.Paragraph.Contains(keyWord))).ToList();
        }

        /// <summary>
        /// Get the documents by page from the history table
        /// </summary>
        /// <param name="keyWord"></param>
        /// <param name="lastPage"></param>
        /// <returns></returns>
        public List<Document> GetDocumentsByPageFromHistory(string keyWord, int lastPage)
        {
            var context = _Db.GetCollection<History>("History");

            var histories = GetListPaginate(context, keyWord, lastPage, Query<History>.EQ(h => h.KeyWord, keyWord));

            return histories.Join(
                _Db.GetCollection<Document>("Document").FindAll(),
                h => h.DocumentId,
                d => d.Id,
                (h, d) => new Document { Id = d.Id, Name = d.Name, Paragraph = d.Paragraph, ViewNumber = d.ViewNumber } ).ToList();
        }

        /// <summary>
        /// Get the list paginate
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <param name="keyWord"></param>
        /// <param name="lastPage"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        private MongoCursor<T> GetListPaginate<T>(MongoCollection<T> context, string keyWord, int lastPage, IMongoQuery query)
        {
            long maxPages = context.Count();
           
            int startSkip = -1;

            if (lastPage > 1)
            {
                startSkip = 10 * (lastPage - 1);
            }

            MongoCursor<T> result = null;

            if(startSkip >= 0)
            {
                result = context.Find(query).SetLimit(10).SetSkip(startSkip);
            }
            else
            {
                result = context.Find(query).SetLimit(10);
            }

            return result;
        }
        #endregion
    }
}
