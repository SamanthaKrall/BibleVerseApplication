using BibleVerseApplication.Services.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BibleVerseApplication.Controllers
{
    public class HomeController : Controller
    {
        [Unity.Dependency]
        public ILogger logger = new MyLogger2();

        public ActionResult Index()
        {
            return View();
        }
    }
}