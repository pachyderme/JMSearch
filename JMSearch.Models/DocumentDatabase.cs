using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

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
        #endregion
    }
}
