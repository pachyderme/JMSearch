﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace JMSearch.Client.Pages
{
    public class ResultInfosModel : PageBase
    {
        public string DocumentParagraph { get; set; }
        public string DocumentName { get; set; }

        public void OnPost()
        {
            DocumentName = Request.Form["documentName"];
            DocumentParagraph = Request.Form["documentParagraph"];

            var documentId = Request.Form["documentId"];

            if (documentId != string.Empty && DocumentName != string.Empty && DocumentParagraph != string.Empty )
            {
                IncrementView(documentId);
            }
        }

        private void IncrementView(string documentId)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    Dictionary<string, string> pairs = new Dictionary<string, string>();
                    pairs.Add("documentId", documentId);
                    FormUrlEncodedContent content = new FormUrlEncodedContent(pairs);

                    HttpResponseMessage response = client.PostAsync("http://192.168.206.145/api/search/PostDocumentView/", content).Result;
                }
                catch(Exception ex)
                {
                    // can't increment the view property
                }
            }
        }
    }
}