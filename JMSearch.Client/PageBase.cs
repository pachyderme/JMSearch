using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JMSearch.Client
{
    public abstract class PageBase : PageModel
    {
        public bool IsConnected { get; set; }
        public bool DisplayLogInActions { get; set; }

        private IMemoryCache _cache;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="memoryCache"></param>
        public PageBase(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }

        /// <summary>
        /// On Get
        /// </summary>
        /// <param name="disconnect"></param>
        /// <param name="currentPageNumber"></param>
        /// <param name="keyWord"></param>
        public virtual void OnGet(bool? disconnect, int currentPageNumber, string keyWord)
        {
            if (disconnect != null)
            {
                if (disconnect == true)
                {
                    HttpContext.Session.Remove("Pseudo");
                    HttpContext.Session.Remove("Password");

                    IsConnected = false;
                }

                DisplayLogInActions = true;
            }
            else
            {
                DisplayLogInActions = false;
            }

            SetConnectedState();
        }

        /// <summary>
        /// Set the connected state
        /// </summary>
        public virtual void SetConnectedState()
        {
            HttpContext.Session.TryGetValue("Pseudo", out byte[] pseudo);

            HttpContext.Session.TryGetValue("Password", out byte[] password);

            if (pseudo != null && password != null)
            {
                IsConnected = true;
            }
            else
            {
                IsConnected = false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public T CacheGetOrCreateAsync<T>(string key)
        {
            var result = _cache.GetOrCreate<T>(key, entry =>
            {
                return entry.Value;
            });

            return result;
        }
    }
}
