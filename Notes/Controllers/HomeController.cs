using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DbConnection;

namespace Notes.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            List<Dictionary<string, object>> AllNotes = DbConnector.Query("SELECT * FROM notes");
            ViewBag.AllNotes = AllNotes;
            return View();
        }

        [HttpPost]
        [Route("grab")]
        public Dictionary<string, object> GrabNote(string title){
            Console.WriteLine("HERE IS THE ITEM");
            Console.WriteLine(title);
            DbConnector.Execute($"INSERT INTO notes (title) VALUES ('{title}')");
            List<Dictionary<string, object>> justAdded = DbConnector.Query("SELECT * FROM notes ORDER BY ID DESC");
            return justAdded[0];
        }

        [HttpPost]
        [Route("update")]
        public string UpdateNote(string content, string id){
            string query = $"UPDATE notes SET content='{content}' WHERE id = {id}";
            DbConnector.Execute(query);
            return content;
        }


        [HttpPost]
        [Route("destroy")]
        public string DestroyNote(string id){
            string query = $"DELETE FROM notes WHERE id = {id}";
            DbConnector.Execute(query);
            return id;
        }

    }
}
