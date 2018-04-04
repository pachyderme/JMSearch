using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JMSearch.Client
{
    public abstract class PageBase: PageModel
    {
        public bool IsConnected { get; set; }
        public bool DisplayLogInActions { get; set; }

        public virtual void OnGet(bool? disconnect, int currentPageNumber, string keyWord, string name, string paragraph, string documentId)
        {
            if(disconnect != null)
            {
                if (disconnect == true)
                {
                    HttpContext.Session.Remove("Pseudo");
                    HttpContext.Session.Remove("Password");

                    IsConnected = false;
                }

                DisplayLogInActions = true;
            }
            else
            {
                DisplayLogInActions = false;
            }

            SetConnectedState();
        }

        public virtual void SetConnectedState()
        {
            HttpContext.Session.TryGetValue("Pseudo", out byte[] pseudo);

            HttpContext.Session.TryGetValue("Password", out byte[] password);

            if (pseudo != null && password != null)
            {
                IsConnected = true;
            }
            else
            {
                IsConnected = false;
            }
        }
    }
}
