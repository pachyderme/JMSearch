using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JMSearch.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JMSearch.API.Controllers
{
    [Produces("application/json")]
    [Route("api/User")]
    public class UserController : Controller
    {
        private DocumentDatabase _DocumentDatabase;

        /// <summary>
        /// GET a User
        /// </summary>
        /// <param name="pseudo"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpGet("{pseudo}/{password}")]
        public User Get(string pseudo, string password)
        {
            Console.WriteLine($" {DateTime.Now} | USER : Get => pseudo : {pseudo}, password : {password}");

            _DocumentDatabase = DocumentDatabase.GetInstance();

            return _DocumentDatabase.GetUserByPseudoAndPassword(pseudo, password);
        }

        /// <summary>
        /// POST a user
        /// </summary>
        /// <param name="pseudo"></param>
        /// <param name="password"></param>
        [HttpPost]
        public void Post(string pseudo, string password)
        {
            Console.WriteLine($" {DateTime.Now} | USER : Post => pseudo : {pseudo}, password : {password}");

            _DocumentDatabase = DocumentDatabase.GetInstance();

            User user = new User { Password = password, Pseudo = pseudo };

            _DocumentDatabase.Create(user);
        }
    }
}