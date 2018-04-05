using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
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
            HttpResponseMessage result;
            using (var client = new HttpClient())
            {
                result = client.GetAsync("http://192.168.206.126:5000/api/documents/"+name).Result;
            }

            return result.Content.ReadAsStreamAsync();
        }
    }
}