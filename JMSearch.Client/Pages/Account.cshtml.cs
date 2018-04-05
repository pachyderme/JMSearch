using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using JMSearch.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace JMSearch.Client.Pages
{
    public class AccountModel : PageBase
    {
        public List<History> Histories { get; set; }

        public AccountModel(IMemoryCache cache) : base(cache)
        {

        }

        public override void OnGet(bool? disconnect, int currentPageNumber, string keyWord)
        {
            base.OnGet(disconnect, currentPageNumber, keyWord);

            using (var client = new HttpClient())
            {
                Histories = new List<History>();

                try
                {
                    HttpContext.Session.TryGetValue("UserId", out byte[] userIdTmp);

                    string userId = Encoding.ASCII.GetString(userIdTmp);

                    HttpResponseMessage response = client.GetAsync(URLHistoryAPI + userId).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        Histories = JsonConvert.DeserializeObject<List<History>>(response.Content.ReadAsStringAsync().Result);
                    }
                }
                catch (Exception ex)
                {
                    // for test
                    //Results.Documents.Add(new Document { Id = "1", Name = "test", Paragraph = "test para", ViewNumber = 0 });
                }
            }
        }
    }
}