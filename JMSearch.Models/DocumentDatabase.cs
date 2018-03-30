using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace JMSearch.Models
{
    public class DocumentDatabase
    {
        private MongoClient _Client;
        private MongoServer _Server;
        private MongoDatabase _Db;

        public DocumentDatabase()
        {
            _Client = new MongoClient("mongodb://192.168.206.125:27017");
            _Server = _Client.GetServer();
            _Db = _Server.GetDatabase("Document");

        }

        public void Create(Document document)
        {
            _Db.GetCollection<Document>("Document").Save(document);
        }
    }
}
