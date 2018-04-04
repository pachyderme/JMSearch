using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using JMSearch.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
            using (var client = new HttpClient())
            {
                Results = new DocumentsPaginate();

                try
                {
                    HttpResponseMessage response = client.GetAsync("http://192.168.206.145/api/search/GetResponses/" + KeyWord + "/" + CurrentPageNumber).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        Results = JsonConvert.DeserializeObject<DocumentsPaginate>(response.Content.ReadAsStringAsync().Result);
                    }
                }
                catch (Exception ex)
                {
                    Results.Documents.Add(new Document { Id = "1", Name = "test", Paragraph = "test para", ViewNumber = 0 });
                }
            }
        }
    }
}