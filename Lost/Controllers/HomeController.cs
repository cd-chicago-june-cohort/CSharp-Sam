using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Lost.Models;
using Lost.Factory;

namespace Lost.Controllers
{
    public class HomeController : Controller
    {

        private readonly TrailFactory trailFactory;
        public HomeController()
        {
            trailFactory = new TrailFactory();
        }
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            ViewBag.trails = trailFactory.FindAll();
            return View();
        }

        [HttpGet]
        [Route("new")]
        public IActionResult add()
        {
            return View();
        }

        [HttpPost]
        [Route("create")]
        public IActionResult Create(Trail item){
            if(ModelState.IsValid)
            {
                trailFactory.Add(item);
                return RedirectToAction("Index");
            } else {
                return View("add", item);
            }
        }

        [HttpGet]
        [Route("trail/{id}")]
        public IActionResult ViewTrail(string id)
        {
            var myTrail = trailFactory.GrabOne(id);
            @ViewBag.trail = myTrail;
            return View("trail");
        }

    }
}
