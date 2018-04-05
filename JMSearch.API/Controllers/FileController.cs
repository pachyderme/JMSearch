using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using JMSearch.API.Properties;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JMSearch.API.Controllers
{
    [Produces("application/json")]
    [Route("api/File")]
    public class FileController : Controller
    {
        /// <summary>
		/// Obtient un pdf en fonction du nom.
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		[Produces(typeof(Stream))]
        [HttpGet("{name}")]
        public Task<Stream> Get(string name)
        {
            Console.WriteLine($" {DateTime.Now} | FILE : Get => name : {name}");

            HttpResponseMessage result;
            using (var client = new HttpClient())
            {
                result = client.GetAsync(Resources.URLBaseDocumentAPI + "?name=" + name).Result;
            }

            return result.Content.ReadAsStreamAsync();
        }
    }
}