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
        public int MyProperty { get; set; }
        public override void OnGet(bool? disconnect, int currentPageNumber, string keyWord, string name, string paragraph)
        {
            base.OnGet(disconnect, currentPageNumber, keyWord, name, paragraph);
        }
    }
}