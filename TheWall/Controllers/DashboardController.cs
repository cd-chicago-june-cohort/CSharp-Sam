using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using TheWall.Models;
using DbConnection;
using Newtonsoft.Json;

namespace TheWall.Controllers
{
    public class DashboardController : Controller
    {
        [HttpPost]
        [Route("create")]
        public IActionResult Create(string post){
            Dictionary<string, object> user = HttpContext.Session.GetObjectFromJson<Dictionary<string, object>>("loggedUser");
            string id = user["id"].ToString();
            string query = $"INSERT INTO posts (content, created_at, user_id) VALUES ('{post}', now(), {id})";
            DbConnector.Execute(query);
            return RedirectToAction("Dashboard", "Home");
        }

        [HttpPost]
        [Route("newcomment")]
        public IActionResult NewComment(string comment, string which_post){
            Dictionary<string, object> user = HttpContext.Session.GetObjectFromJson<Dictionary<string, object>>("loggedUser");
            string id = user["id"].ToString();
            string query = $"INSERT INTO comments (content, created_at, post_id, user_id) VALUES ('{comment}', now(),{which_post}, {id})";
            DbConnector.Execute(query);
            Console.WriteLine(query);
            return RedirectToAction("Dashboard", "Home");
        }

    }
}