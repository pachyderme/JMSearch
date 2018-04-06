using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using JMSearch.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace JMSearch.Client.Pages
{
    public class LoginModel : PageBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="cache"></param>
        public LoginModel(IMemoryCache cache) : base(cache)
        {

        }

        /// <summary>
        /// OnPost
        /// </summary>
        /// <returns></returns>
        public IActionResult OnPost()
        {
            string pseudo = Request.Form["pseudo"].ToString();
            string password = Request.Form["password"].ToString();

            SHA256 sha256 = SHA256.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(password);
            byte[] passwordCrypted = sha256.ComputeHash(bytes);
            string passwordHashed = GetStringFromHash(passwordCrypted);

            string pageResult = "Login";

            if (Request.Form["subscribe"].Count > 0 && CreateUser(pseudo, passwordHashed))
            {
                pageResult = "Index";
            }

            if (pageResult == "Index" || Request.Form["connect"].Count > 0)
            {
                if(CheckUserExists(pseudo, passwordHashed))
                {
                    pageResult = "Index";
                }
            }


            return RedirectToPage(pageResult);
        }

        /// <summary>
        /// Get string from the hash
        /// </summary>
        /// <param name="hash"></param>
        /// <returns></returns>
        private static string GetStringFromHash(byte[] hash)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }
            return result.ToString();
        }

        /// <summary>
        /// Create the user
        /// </summary>
        /// <param name="pseudo"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private bool CreateUser(string pseudo, string password)
        {
            bool result = false;

            using (var client = new HttpClient())
            {
                try
                {
                    Dictionary<string, string> pairs = new Dictionary<string, string>();
                    pairs.Add("pseudo", pseudo);
                    pairs.Add("password", password);
                    FormUrlEncodedContent content = new FormUrlEncodedContent(pairs);

                    HttpResponseMessage response = client.PostAsync(URLUserAPI, content).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        result = true;
                    }
                }
                catch (Exception ex)
                {
                    // for test
                }
            }

            return result;
        }

        /// <summary>
        /// Check if the current useralready exist
        /// </summary>
        /// <param name="pseudo"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private bool CheckUserExists(string pseudo, string password)
        {
            bool result = false;

            using (var client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = client.GetAsync(URLUserAPI + pseudo + "/" + password.ToString()).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        User user = JsonConvert.DeserializeObject<User>(response.Content.ReadAsStringAsync().Result);

                        if (user != null)
                        {
                            HttpContext.Session.Set("Pseudo", Encoding.ASCII.GetBytes(user.Pseudo));
                            HttpContext.Session.Set("Password", Encoding.ASCII.GetBytes(user.Password));
                            HttpContext.Session.Set("UserId", Encoding.ASCII.GetBytes(user.Id));

                            result = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    // for test
                }
            }

            return result;
        }
    }
}