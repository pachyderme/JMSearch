using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

namespace JMSearch.Client.Pages
{
    public class ResultInfosModel : PageBase
    {
        public string DocumentPage { get; set; }
        public string DocumentName { get; set; }


        public ResultInfosModel(IMemoryCache cache) : base(cache)
        {

        }

        /// <summary>
        /// OnPost
        /// </summary>
        public void OnPost()
        {
            DocumentName = Path.GetFileNameWithoutExtension(Request.Form["documentName"]);
            DocumentPage = Request.Form["documentPage"];

            var documentId = Request.Form["documentId"];
            var keyWord = Request.Form["keyWord"];

            if (documentId != string.Empty && DocumentName != string.Empty && DocumentPage != string.Empty  && keyWord != string.Empty)
            {
                IncrementView(documentId);
                AddHistory(documentId, keyWord);
                ViewData["DocumentScript"] = true;
            }
        }

        private void AddHistory(StringValues documentId, StringValues keyWord)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    HttpContext.Session.TryGetValue("UserId", out byte[] userIdTmp);

                    string userId = Encoding.ASCII.GetString(userIdTmp);

                    Dictionary<string, string> pairs = new Dictionary<string, string>();
                    pairs.Add("documentId", documentId);
                    pairs.Add("keyWord", keyWord);
                    pairs.Add("userId", userId);
                    FormUrlEncodedContent content = new FormUrlEncodedContent(pairs);

                    HttpResponseMessage response = client.PostAsync(URLHistoryAPI, content).Result;
                }
                catch (Exception ex)
                {
                    // can't increment the view property
                }
            }
        }

        /// <summary>
        /// Increment the view for the document
        /// </summary>
        /// <param name="documentId"></param>
        private void IncrementView(string documentId)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    Dictionary<string, string> pairs = new Dictionary<string, string>();
                    pairs.Add("documentId", documentId);
                    FormUrlEncodedContent content = new FormUrlEncodedContent(pairs);

                    HttpResponseMessage response = client.PostAsync(URLPostDocumentView, content).Result;
                }
                catch(Exception ex)
                {
                    // can't increment the view property
                }
            }
        }
    }
}