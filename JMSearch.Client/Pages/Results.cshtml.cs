﻿using System;
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
            KeyWord = keyWord;

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
            KeyWord = Request.Form["keyWord"];

            SetResults();

            if (Request.Form["JMHelp"].ToString() != string.Empty)
            {
                ViewData["JMHelp"] = true;
                // JMHelp Mode
            }
        }

        private void SetResults()
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync("http://localhost:5000/api/search/GetResponses/" + KeyWord).Result;
                Results = JsonConvert.DeserializeObject<DocumentsPaginate>(response.Content.ReadAsStringAsync().Result);
            }
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