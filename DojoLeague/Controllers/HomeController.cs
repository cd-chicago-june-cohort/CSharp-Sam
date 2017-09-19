using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DojoLeague.Factory;
using DojoLeague.Models;

namespace DojoLeague.Controllers
{
    public class HomeController : Controller
    {
        private readonly NinjaFactory ninjaFactory;
        private readonly DojoFactory dojoFactory;
        public HomeController()
        {
            ninjaFactory = new NinjaFactory();
            dojoFactory = new DojoFactory();
        }
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult NinjaIndex()
        {
            ViewBag.dojoNinjas = ninjaFactory.HomeInfo();
            ViewBag.rogues = ninjaFactory.GrabRogues();
            ViewBag.dojos = dojoFactory.FindAll();
            return View("NinjaIndex");
        }

        [HttpGet]
        [Route("dojos")]
        public IActionResult DojoIndex()
        {
            @ViewBag.dojos = dojoFactory.FindAll();
            return View("DojoIndex");
        }

        [HttpPost]
        [Route("createNinja")]
        public IActionResult CreateNinja(Ninja item, int myDojo)
        {   
            ninjaFactory.Add(item, myDojo);
            return RedirectToAction("NinjaIndex");
        }

        [HttpPost]
        [Route("createDojo")]
        public IActionResult CreateDojo(Dojo item)
        {   
            dojoFactory.Add(item);
            return RedirectToAction("DojoIndex");
        }

        [HttpGet]
        [Route("ninja/{myId}")]
        public IActionResult GrabNinja(int myId)
        {   
            ViewBag.myNinja = ninjaFactory.GrabOne(myId);
            try {
                ViewBag.currentDojo = ninjaFactory.CurrentDojo(myId);
                ViewBag.rogue = false;
            }
            catch{
                ViewBag.rogue = true;
            }
            return View("ninja");
        }

        [HttpGet]
        [Route("dojo/{myId}")]
        public IActionResult GrabDojo(int myId)
        {   
            ViewBag.myDojo = dojoFactory.GrabOne(myId);
            var test = dojoFactory.GrabOne(myId);
            ViewBag.rogues = ninjaFactory.GrabRogues();
            ViewBag.myNinjas = dojoFactory.AllNinjas(myId);
            return View("dojo");
        }

        [HttpGet]
        [Route("banish/{ninjaId}/{dojoId}")]
        public IActionResult Banish(int ninjaId, int dojoId){
            dojoFactory.Banish(ninjaId, dojoId);
            return RedirectToAction("GrabDojo", new { myId = dojoId });
        }

        [HttpGet]
        [Route("recruit/{ninjaId}/{dojoId}")]
        public IActionResult Recruit(int ninjaId, int dojoId){
            dojoFactory.Recruit(ninjaId, dojoId);
            return RedirectToAction("GrabDojo", new { myId = dojoId });
        }

    }
}
