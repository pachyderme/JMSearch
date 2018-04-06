using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;

namespace JMSearch.Client.Pages
{
    public class EasterEggModel : PageBase
    {
        public EasterEggModel(IMemoryCache memoryCache) : base(memoryCache)
        {
        }
    }
}