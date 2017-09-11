using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;
// using JsonData;
using Microsoft.AspNetCore.Http;

namespace Survey.Controllers {

    
    public class SurveyController : Controller {
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View("index");
        }

        [HttpPost]
        [Route("survey")]
        public IActionResult Survey(string name, string location, string lang, string comment)
        {
            ViewBag.Name = name;
            ViewBag.Loc = location;
            ViewBag.Lang = lang;
            ViewBag.Comment = comment;
            return View("results");
        }
    }
}