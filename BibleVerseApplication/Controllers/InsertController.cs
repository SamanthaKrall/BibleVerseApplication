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
    public class InsertController : Controller
    {
        [Unity.Dependency]
        public ILogger logger = new MyLogger2();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Insert(VerseModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "This verse has already been added";
                return View("Index");
            }

            VerseService service = new VerseService(model);
            service.InsertVerse();
            ViewBag.Message = "Successfully added " + model.Book + " " + model.Chapter + ":" + model.Verse + " to the database";
            return View("Index");
        }
    }
}