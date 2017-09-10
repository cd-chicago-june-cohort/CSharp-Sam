using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;
// using JsonData;
using Microsoft.AspNetCore.Http;

namespace CurrentTime.Controllers {

    
    public class TimeController : Controller {
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View("index");
        }
    }
}