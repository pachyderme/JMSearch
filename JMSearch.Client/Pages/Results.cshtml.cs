using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using JMSearch.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JMSearch.Client.Pages
{
    public class ResultsModel : PageBase
    {
        public DocumentsPaginate Results { get; set; }

        #region UIModel

        public string KeyWord { get; set; }

        public int NumberItemPerPage { get; set; }

        public int CurrentPageNumber { get; set; }

        #endregion

        public ResultsModel(IMemoryCache cache) : base(cache)
        {

        }

        public override void OnGet(bool? disconnect, int currentPageNumber, string keyWord)
        {
            base.OnGet(disconnect, currentPageNumber, keyWord);
            DisplayLogInActions = true;
            SetConnectedState();

            CurrentPageNumber = currentPageNumber;
            KeyWord = keyWord?.Trim();

            SetResults();
        }

        /// <summary>
        /// OnPost
        /// </summary>
        public void OnPost()
        {
            DisplayLogInActions = true;
            SetConnectedState();

            CurrentPageNumber = 1;
            KeyWord = Request.Form["keyWord"].ToString()?.Trim();

            SetResults();

            if (Request.Form["JMHelp"].ToString() != string.Empty)
            {
                ViewData["JMHelp"] = true;
                // JMHelp Mode
            }
        }

        /// <summary>
        /// Set the results from API
        /// </summary>
        private void SetResults()
        {
            var value = GetCachedValue<DocumentsPaginate>(KeyWord);

            if (value != null)
            {
                Results = value;
            }
            else
            {
                using (var client = new HttpClient())
                {
                    Results = new DocumentsPaginate();

                    try
                    {
                        var watch = new Stopwatch();
                        watch.Start();
                        HttpResponseMessage response = client.GetAsync(URLGetResultsAPI + KeyWord + "/" + CurrentPageNumber).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            Results = JsonConvert.DeserializeObject<DocumentsPaginate>(response.Content.ReadAsStringAsync().Result);

                            SetCachedValue(KeyWord, Results);
                        }

                        watch.Stop();

                        Console.WriteLine(watch.ElapsedMilliseconds);
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
}