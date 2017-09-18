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
    public class HomeController : Controller
    {
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("success")!=null){
                ViewBag.success = HttpContext.Session.GetString("success");
            }
            return View();
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(User user)
        {
            ViewBag.success="";
            Console.WriteLine("MADE IT INSIDE REGISTER FUNCTION");
            var unique = true;
            List<Dictionary<string, object>> AllUsers = DbConnector.Query("SELECT * FROM users");
            foreach(var entry in AllUsers){
                if (user.Email == entry["email"].ToString()){
                    unique = false;
                }
            }
            if(ModelState.IsValid && unique)
            {
                HttpContext.Session.SetString("success", "Success! You can now log in.");
                string query = $"INSERT INTO users (first_name, last_name, email, password, created_at) VALUES ('{user.First}', '{user.Last}', '{user.Email}', '{user.Password}', now())";
                DbConnector.Execute(query);
                return RedirectToAction("Index");
            }
            if (!unique){
                ViewBag.existing = "Email address is already registered";
            }
            return View("Index", user);
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(string email, string password)
        {
            List<Dictionary<string, object>> AllUsers = DbConnector.Query("SELECT * FROM users");
            bool existing = false;
            bool correctPW = false;
            foreach(var entry in AllUsers){
                if (email == entry["email"].ToString()){
                    existing = true;
                    if (password == entry["password"].ToString()) {
                        //Success stuff goes in here
                        HttpContext.Session.SetObjectAsJson("loggedUser", entry);
                        return RedirectToAction("Dashboard");
                    }
                }
            }
            if (!existing){
                HttpContext.Session.SetString("notreg", "Email not registered. You must register before logging in.");
                ViewBag.notreg = HttpContext.Session.GetString("notreg");
            }
            if (!correctPW){
                HttpContext.Session.SetString("wrongpw", "Incorrect password. Please try again.");
                ViewBag.wrongpw = HttpContext.Session.GetString("wrongpw");
            }
            return View("Index");
        }

        [HttpGet]
        [Route("dashboard")]
        public IActionResult Dashboard(){
            ViewBag.user = HttpContext.Session.GetObjectFromJson<Dictionary<string, object>>("loggedUser");
            string postquery = "SELECT concat(first_name, ' ', last_name) as name, posts.created_at as date, posts.content, posts.id as post_id, users.id as user_id FROM users JOIN posts ON users.id=posts.user_id ORDER BY posts.id DESC";
            var results = DbConnector.Query(postquery);
            ViewBag.results = results;
            string commentquery = "SELECT concat(first_name, ' ', last_name) as name, comments.created_at as date, comments.content as comment, comments.post_id as post_id, users.id as user_id FROM users JOIN comments ON users.id=comments.user_id ORDER BY comments.created_at DESC";
            var comments = DbConnector.Query(commentquery);
            ViewBag.comments = comments;
            return View();
        }

        [HttpGet]
        [Route("logout")]
        public IActionResult Logout(){
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }


    }

    public static class SessionExtensions
    {
        // We can call ".SetObjectAsJson" just like our other session set methods, by passing a key and a value
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            // This helper function simply serializes theobject to JSON and stores it as a string in session
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
        
        // generic type T is a stand-in indicating that we need to specify the type on retrieval
        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            string value = session.GetString(key);
            // Upon retrieval the object is deserialized based on the type we specified
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }

}