using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JMSearch.Client.Pages
{
    public class ResultsModel : PageModel
    {
        private List<Result> Results { get; set; }

        #region UIModel

        public string KeyWord { get; set; }

        public IEnumerable<IGrouping<string, Result>> ResultsUI { get; set; }

        public int MaxPage { get; set; }

        public int NumberItemPerPage { get; set; }

        public int CurrentPageNumber { get; set; }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        public void OnGet(int currentPageNumber, string keyWord)
        {
            CurrentPageNumber = currentPageNumber;
            KeyWord = keyWord;

            GetResults();
        }

        /// <summary>
        /// 
        /// </summary>
        public void OnPost()
        {
            CurrentPageNumber = 1;

            KeyWord = Request.Form["keyWord"];


            if (Request.Form["JMHelp"].ToString() != string.Empty)
            {
                ViewData["JMHelp"] = true;
                // JMHelp Mode
            }

            GetResults();

        }

        /// <summary>
        /// 
        /// </summary>
        private void GetResults()
        {
            NumberItemPerPage = 2;

            if (KeyWord != null && KeyWord.Trim() != string.Empty)
            {
                Results = new List<Result>
                {
                    new Result{ Content = "Toto1", DocumentName="Doc1"},
                    new Result{ Content = "Toto2", DocumentName="Doc1"},
                    new Result{ Content = "Toto3", DocumentName="Doc2"},
                    new Result{ Content = "Toto4", DocumentName="Doc3"},
                    new Result{ Content = "Toto5", DocumentName="Doc3"},
                    new Result{ Content = "Toto6", DocumentName="Doc3"}
                };
            }
            else
            {
                Results = new List<Result>();
            }

            ResultsUI = Results.GroupBy(r => r.DocumentName);

            MaxPage = (int)Math.Ceiling((double)ResultsUI.Count() / NumberItemPerPage);

            int startSkip = -1;

            if (CurrentPageNumber > 1)
            {
                startSkip = NumberItemPerPage * (CurrentPageNumber - 1);
            }

            ResultsUI = ResultsUI.Skip(startSkip).Take(NumberItemPerPage);
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