using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JMSearch.Client.Pages
{
    public class LoginModel : PageBase
    {
        public IActionResult OnPost()
        {
            string pseudo = Request.Form["pseudo"].ToString();
            HttpContext.Session.Set("Pseudo", Encoding.ASCII.GetBytes(pseudo));
            string password = Request.Form["password"].ToString();
            HttpContext.Session.Set("Password", Encoding.ASCII.GetBytes(password));

            return RedirectToPage("/Index");
        }
    }
}