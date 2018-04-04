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
            _DocumentDatabase = new DocumentDatabase();

            return _DocumentDatabase.GetDocumentByPage(keyWord, currentPage);
        }
    }
}