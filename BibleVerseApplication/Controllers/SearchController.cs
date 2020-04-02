using BibleVerseApplication.Models;
using BibleVerseApplication.Services.Business;
using BibleVerseApplication.Services.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BibleVerseApplication.Controllers
{
    public class SearchController : Controller
    {
        [Unity.Dependency]
        public ILogger logger = new MyLogger2();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search(VerseModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index");
            }
            VerseService service = new VerseService(model);
            List<string> text = service.SearchVerses();
            return View("Results", text);
        }
    }
}