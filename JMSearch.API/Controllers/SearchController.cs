using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JMSearch.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Search")]
    public class SearchController : Controller
    {
        /// <summary>
        /// GET the responses from a keyword
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        [HttpGet("GetResponses/{keyWord}")]
        public string GetResponses(string keyWord)
        {
            return "good";
        }
    }
}