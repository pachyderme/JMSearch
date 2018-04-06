using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JMSearch.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JMSearch.API.Controllers
{
    [Produces("application/json")]
    [Route("api/History")]
    public class HistoryController : Controller
    {
        private DocumentDatabase _DocumentDatabase;

        /// <summary>
        /// GET a history
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("{userId}")]
        public List<History> Get(string userId)
        {
            Console.WriteLine($" {DateTime.Now} | HISTORY : Get => userId : {userId}");

            _DocumentDatabase = DocumentDatabase.GetInstance();

            return _DocumentDatabase.GetHistoriesByUserId(userId);
        }

        /// <summary>
        /// POST a history
        /// </summary>
        /// <param name="documentId"></param>
        /// <param name="keyWord"></param>
        /// <param name="userId"></param>
        [HttpPost]
        public void Post(string documentId, string keyWord, string userId)
        {
            Console.WriteLine($" {DateTime.Now} | HISTORY : Post => documentId : {documentId}, keyWord : {keyWord}, userId : {userId}");

            _DocumentDatabase = DocumentDatabase.GetInstance();

            History history = new History { KeyWord = keyWord, UserId = userId };

            _DocumentDatabase.Create(history);
        }
    }
}