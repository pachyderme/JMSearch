using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JMSearch.Client.Pages
{
    public class ResultInfosModel : PageBase
    {
        public string Name { get; set; }
        public string Paragraph { get; set; }
        public override void OnGet(bool? disconnect, int currentPageNumber, string keyWord)
        {
            base.OnGet(disconnect, currentPageNumber, keyWord);


        }
    }
}