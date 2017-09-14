using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Movies.Controllers
{
    public class HomeController : Controller
    {

        private readonly DbConnector _dbConnector;
 
        public HomeController(DbConnector connect)
        {
            _dbConnector = connect;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            List<Dictionary<string, object>> AllMovies = _dbConnector.Query("SELECT * FROM movies ORDER BY created_at DESC");
            ViewBag.movies = AllMovies;
            return View();
        }

        [HttpPost]
        [Route("search")]
        public JsonResult Search(Dictionary<string, string> movie){
            string title = movie["title"];
            double rating = Convert.ToDouble(movie["vote_average"]);
            string sqlDate = movie["release_date"];
            string insert = $"INSERT INTO movies (title, rating, release_date, created_at) VALUES ('{title}', {rating}, '{sqlDate}', now())";
            _dbConnector.Execute(insert);
            DateTime date = Convert.ToDateTime(sqlDate);
            string returnDate = date.ToString("MMMM dd, yyyy");
            Console.WriteLine(returnDate);
            var response = new {
                myTitle = title,
                myRating = rating.ToString(),
                myDate = returnDate
            };
            return Json(response);
        }

    }
}
