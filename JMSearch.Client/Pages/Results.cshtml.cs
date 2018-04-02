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
        public string KeyWord { get; set; }
        private List<Result> Results { get; set; }

        public IEnumerable<IGrouping<string, Result>> ResultsUI { get; set; }

        public int MaxPage { get; set; }

        public int NumberItemPerPage { get; set; }

        public int CurrentPageNumber { get; set; }

        public void OnGet()
        {

        }

        public void OnPost()
        {
            NumberItemPerPage = 2;

            byte[] isNextPage;
            HttpContext.Session.TryGetValue("isNextPage", out isNextPage);

            if (isNextPage == null)
            {
                CurrentPageNumber = 1;
                HttpContext.Session.Remove("isNextPage");
            }
            else
            {
                byte[] currentPageNumberTmp;
                HttpContext.Session.TryGetValue("CurrentPageNumber", out currentPageNumberTmp);
                HttpContext.Session.Remove("CurrentPageNumber");

                CurrentPageNumber = Convert.ToInt32(currentPageNumberTmp);
            }

            byte[] keyWordTmp;
            HttpContext.Session.TryGetValue("KeyWord", out keyWordTmp);

            if(keyWordTmp == null)
            {
                KeyWord = Convert.ToString(keyWordTmp);
                HttpContext.Session.Remove("KeyWord");
            }
            else
            {
                KeyWord = Request.Form["keyWord"];
            }

            Results = new List<Result>
            {
                new Result{ Content = "Toto1", DocumentName="Doc1"},
                new Result{ Content = "Toto2", DocumentName="Doc1"},
                new Result{ Content = "Toto3", DocumentName="Doc2"},
                new Result{ Content = "Toto4", DocumentName="Doc3"},
                new Result{ Content = "Toto5", DocumentName="Doc3"},
                new Result{ Content = "Toto6", DocumentName="Doc3"}
            };

            ResultsUI = Results.GroupBy(r => r.DocumentName);

            MaxPage = (int)Math.Ceiling((double)ResultsUI.Count() / NumberItemPerPage);

            int startSkip = -1;

            if(CurrentPageNumber > 1)
            {
                startSkip = NumberItemPerPage * CurrentPageNumber - 1;
            }

            ResultsUI = ResultsUI.Skip(startSkip).Take(NumberItemPerPage);
        }

        public RedirectToActionResult NextPage()
        {
            HttpContext.Session.Set("isNextPage", BitConverter.GetBytes(true));
            HttpContext.Session.Set("CurrentPageNumber", BitConverter.GetBytes(++CurrentPageNumber));
            HttpContext.Session.Set("KeyWord", Encoding.ASCII.GetBytes(KeyWord));

            return RedirectToAction("OnPost");
        }
    }

    public class Result
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string DocumentName { get; set; }
    }
}