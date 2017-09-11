using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;
// using JsonData;
using Microsoft.AspNetCore.Http;

namespace RandomPasscode.Controllers {

    
    public class RandomController : Controller {
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            HttpContext.Session.SetInt32("count", 0);
            ViewBag.count = HttpContext.Session.GetInt32("count");
            return View("index");
        }

        [HttpGet]
        [Route("generate")]
        public string generate()
        {
            Random rand = new Random();
            string[] characters = {"1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "A", "B", "C", "D", "E", "F", "G", "H", "I", "G", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"};
            string code = "";
            int selection;
            for (int i = 1; i <= 14; i += 1){
                selection = rand.Next(0,36);
                code += characters[selection];
            }
            return code;
        }

        [HttpGet]
        [Route("count")]
        public string Count() {
            int? ogCount = HttpContext.Session.GetInt32("count");
            ogCount += 1;
            ViewBag.count = ogCount;
            HttpContext.Session.SetInt32("count", (int)ogCount);
            return ogCount.ToString();
        }
    }
}