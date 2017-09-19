using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Restaurant.Models;
using System.Linq;

namespace Restaurant.Controllers
{
    public class HomeController : Controller
    {

         private RestaurantContext _context;
 
        public HomeController(RestaurantContext context)
        {
            _context = context;
        }
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("reviews")]
        public IActionResult Landing()
        {
            List<Review> allReviews = _context.reviews.OrderByDescending(review => review.id).ToList();
            @ViewBag.reviews = allReviews;
            return View();
        }

        [HttpPost]
        [Route("create")]
        public IActionResult CreateReview(Review myReview)
        {
            _context.Add(myReview);
            _context.SaveChanges();
            return RedirectToAction("Landing");
        }

    }
}
