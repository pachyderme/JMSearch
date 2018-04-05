using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JMSearch.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace JMSearch.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Search")]
    public class SearchController : Controller
    {
        private DocumentDatabase _DocumentDatabase;

        /// <summary>
        /// GET the responses from a keyword
        /// </summary>
        /// <param name="keyWord"></param>
        /// <param name="currentPage"></param>
        /// <returns></returns>
        [HttpGet("GetResponses/{keyWord}/{currentPage}")]
        public DocumentsPaginate GetResponses(string keyWord, int currentPage)
        {
            Console.WriteLine($" {DateTime.Now} | SEARCH : GetResponses => keyword : {keyWord}, currentPage : {currentPage}");

            _DocumentDatabase = new DocumentDatabase();

            return _DocumentDatabase.GetDocumentByPage(keyWord, currentPage);
        }

        /// <summary>
        /// POST the view for a document
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns></returns>
        [HttpPost("PostDocumentView")]
        public void PostDocumentView(string documentId)
        {
            Console.WriteLine($" {DateTime.Now} | SEARCH : PostDocumentView => documentId : {documentId}");

            _DocumentDatabase = new DocumentDatabase();

            _DocumentDatabase.IncrementViewDocument(documentId);
        }
    }
}