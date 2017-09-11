using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;
// using JsonData;
using Microsoft.AspNetCore.Http;

namespace Portfolio.Controllers {

    
    public class PortfolioController : Controller {
        [HttpGet]
        [Route("")]
        public IActionResult Home()
        {
            return View("home");
        }

        [HttpGet]
        [Route("contact")]
        public IActionResult Contact()
        {
            return View("contact");
        }

        [HttpGet]
        [Route("projects")]
        public IActionResult Projects()
        {
            return View("projects");
        }
    }
}