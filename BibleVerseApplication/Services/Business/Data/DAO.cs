using BibleVerseApplication.Models;
using BibleVerseApplication.Services.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BibleVerseApplication.Services.Business.Data
{
    public class DAO
    {
        private readonly SqlConnection conn = new SqlConnection("data source=(localdb)\\MSSQLLocalDB; database=Test; integrated security = SSPI");
        private string testament, book, text;
        private int chapter, verse;

        [Unity.Dependency]
        public ILogger logger = new MyLogger2();

        public DAO(VerseModel model)
        {
            this.testament = model.Testament;
            this.book = model.Book;
            this.chapter = model.Chapter;
            this.verse = model.Verse;
            this.text = model.Text;
        }

        public bool CheckExisting()
        {
            try
            {
                logger.Info("Entered CheckExisting Method");

                string query = "SELECT * FROM dbo.Verses WHERE Book=@book AND Verse=@verse AND Text=@text";

                SqlCommand cmd = new SqlCommand(query, this.conn);
                cmd.Parameters.Add("@book", SqlDbType.NVarChar, 50).Value = book;
                cmd.Parameters.Add("@verse", SqlDbType.Int).Value = verse;
                cmd.Parameters.Add("@text", SqlDbType.NVarChar).Value = text;

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    conn.Close();
                    logger.Info("Verse already exists in database.");
                    return false;
                }
                else
                {
                    conn.Close();
                    logger.Info("Verse not in databse.");
                    return true;
                }
            }
            catch (SqlException e)
            {
                logger.Error("Error " + e.ToString());
                throw e;
            }
        }

        public void InsertVerse()
        {
            try
            {
                logger.Info("Entered InsertVerse Method");

                string query = "INSERT INTO dbo.Verses (Testament, Book, Chapter, Verse, Text) VALUES (@testament," +
                    " @book, @chapter, @verse, @text)";

                SqlCommand cmd = new SqlCommand(query, this.conn);
                cmd.Parameters.Add("@testament", SqlDbType.NVarChar, 50).Value = testament;
                cmd.Parameters.Add("@book", SqlDbType.NVarChar, 50).Value = book;
                cmd.Parameters.Add("@chapter", SqlDbType.Int).Value = chapter;
                cmd.Parameters.Add("@verse", SqlDbType.Int).Value = verse;
                cmd.Parameters.Add("@text", SqlDbType.NVarChar).Value = text;

                conn.Open();
                cmd.ExecuteNonQuery();
                logger.Info("Verse Inserted");
                conn.Close();
            }
            catch (SqlException e)
            {
                logger.Error("Error: " + e.ToString());
                throw e;
            }

        }

        public List<string> SearchVerse()
        {
            try
            {
                logger.Info("Entered SearchVerse Method");

                string query = "SELECT * FROM dbo.Verses WHERE Testament=@testament AND Book=@book AND Chapter=@chapter" +
                    " AND Verse=@verse";

                SqlCommand cmd = new SqlCommand(query, this.conn);
                cmd.Parameters.Add("@testament", SqlDbType.NVarChar, 50).Value = testament;
                cmd.Parameters.Add("@book", SqlDbType.NVarChar, 50).Value = book;
                cmd.Parameters.Add("@chapter", SqlDbType.Int).Value = chapter;
                cmd.Parameters.Add("@verse", SqlDbType.Int).Value = verse;

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                List<string> list = new List<string>();

                if (!reader.HasRows)
                {
                    list.Add("No results found!");
                    logger.Info("No results found");
                    return list;
                }

                while (reader.Read())
                {
                    list.Add(reader["Testament"].ToString());
                    list.Add(reader["Book"].ToString());
                    list.Add(reader["Chapter"].ToString());
                    list.Add(reader["Verse"].ToString());
                    list.Add(reader["Text"].ToString());
                }

                conn.Close();
                return list;
            }
            catch (SqlException e)
            {
                logger.Error("Error: " + e.ToString());
                throw e;
            }
        }
    }
}