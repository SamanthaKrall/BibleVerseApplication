using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BibleVerseApplication.Services.Business;
using BibleVerseApplication.Models;
using BibleVerseApplication.Services.Utility;
using Unity;
using BibleVerseApplication.Services.Business.Data;

namespace BibleVerseApplication.Services.Business
{
    public class VerseService
    {
        private DAO dao;

        [Dependency]
        public ILogger logger = new MyLogger2();

        public VerseService(VerseModel model)
        {
            dao = new DAO(model);
        }

        public List<string> SearchVerses()
        {
            logger.Info("Entered SearchVerses Method");
            List<string> verse = dao.SearchVerse();
            if (verse[0] != "No results found!")
            {
                logger.Info("Verse: " + verse[4]);
            }
            else
            {
                logger.Info("No results found");
            }
            return verse;
        }

        public bool InsertVerse()
        {
            logger.Info("Entered InsertVerse Method");
            if (dao.CheckExisting())
            {
                dao.InsertVerse();
                logger.Info("Added verse");
                return true;
            }
            else
            {
                logger.Error("Verse already exists");
                return false;
            }
        }
    }
}