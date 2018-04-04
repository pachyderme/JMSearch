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
            KeyWord = keyWord.Trim();

            SetResults();
        }

        /// <summary>
        /// 
        /// </summary>
        public void OnPost()
        {
            DisplayLogInActions = true;
            SetConnectedState();

            CurrentPageNumber = 1;
            KeyWord = Request.Form["keyWord"].ToString().Trim();

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
                HttpResponseMessage response = client.GetAsync("http://localhost:5000/api/search/GetResponses/" + KeyWord + "/" + CurrentPageNumber).Result;

                if (response.IsSuccessStatusCode)
                {
                    Results = JsonConvert.DeserializeObject<DocumentsPaginate>(response.Content.ReadAsStringAsync().Result);
                }
                else
                {
                    Results = new DocumentsPaginate();
                }
            }
        }

        public IActionResult GetDocumentInfos(string name, string paragraph)
        {
            HttpContext.Session.Set("Name", Encoding.ASCII.GetBytes(name));
            HttpContext.Session.Set("Paragraph", Encoding.ASCII.GetBytes(name));

            return RedirectToPage("ResultInfos");
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Result
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string DocumentName { get; set; }
    }
}