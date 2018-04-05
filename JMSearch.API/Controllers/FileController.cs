using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using JMSearch.API.Properties;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace JMSearch.API.Controllers
{
    [Produces("application/json")]
    [Route("api/File")]
    public class FileController : Controller
    {
        public IMemoryCache Cache { get; set; }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="memoryCache"></param>
        public FileController(IMemoryCache memoryCache)
        {
            Cache = memoryCache;
        }

        /// <summary>
		/// Obtient un pdf en fonction du nom.
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		[Produces(typeof(Stream))]
        [HttpGet("{name}")]
        public Stream Get(string name)
        {
            Console.WriteLine($" {DateTime.Now} | FILE : Get => name : {name}");

            HttpResponseMessage response;
            Stream result = GetCachedValue<Stream>(name);

            if(result == null)
            {
                using (var client = new HttpClient())
                {
                    response = client.GetAsync(Resources.URLBaseDocumentAPI + "?name=" + name).Result;
                }

                result = response.Content.ReadAsStreamAsync().Result;

                SetCachedValue(name, result);
            }
            
            return result;
        }

        /// <summary>
        /// Get a cached value
        /// </summary>
        /// <returns></returns>
        private T GetCachedValue<T>(string key)
        {
            return Cache.Get<T>(key);
        }

        /// <summary>
        /// Set a cached value
        /// </summary>
        /// <returns></returns>
        private T SetCachedValue<T>(string key, T value, int expirationTime = 30)
        {
            return Cache.Set<T>(key, value, DateTime.Now.AddMinutes(expirationTime));
        }
    }
}