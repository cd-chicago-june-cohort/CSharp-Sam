using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;
// using JsonData;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Dojodachi.Controllers {

    public static class SessionExtensions
    {
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            string value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }

    public class Dojodachi {
        public int happiness;
        public int fullness;
        public int energy;
        public int meals;
        public Dojodachi(){
            happiness = 20;
            fullness = 20;
            energy = 50;
            meals = 3;
        }


    }
    
    public class CritterController : Controller {
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            ViewBag.showFeed = true;
            ViewBag.win = false;
            ViewBag.lose = false;
            if (HttpContext.Session.GetString("message") == null) {
                HttpContext.Session.SetString("message", "Welcome! Select an action to begin playing with your Dojodachi.");
                ViewBag.message = HttpContext.Session.GetString("message");
                Dojodachi critter = new Dojodachi();
                HttpContext.Session.SetObjectAsJson("myCritter", critter);
                TempData["fullness"] = critter.fullness;
                TempData["happiness"] = critter.happiness;
                TempData["energy"] = critter.energy;
                TempData["meals"] = critter.meals;
            } else {
                Dojodachi critter = HttpContext.Session.GetObjectFromJson<Dojodachi>("myCritter");
                TempData["fullness"] = critter.fullness;
                TempData["happiness"] = critter.happiness;
                TempData["energy"] = critter.energy;
                TempData["meals"] = critter.meals;
                ViewBag.message = HttpContext.Session.GetString("message");
                if (critter.meals == 0) {
                    ViewBag.showFeed = false;
                }
                if (critter.fullness >= 100 && critter.happiness >= 100 && critter.energy >= 100) {
                    ViewBag.win = true;
                    ViewBag.message = "Congratulations! Your Dojodachi is living its best life! You WIN";
                }
                if (critter.fullness == 0) {
                    ViewBag.lose = true;
                    ViewBag.message = "You let your Dojodachi starve... You LOSE";
                }
                if (critter.happiness == 0) {
                    ViewBag.lose = true;
                    ViewBag.message = "Your Dojodachi cried itself to death... You LOSE";
                }
                if (critter.energy == 0) {
                    ViewBag.lose = true;
                    ViewBag.message = "Your Dojodachi died of exhaustion... You LOSE";
                }
            }
            return View("index");
        }

        [HttpGet]
        [Route("feed")]
        public IActionResult Feed(){
            Dojodachi critter = HttpContext.Session.GetObjectFromJson<Dojodachi>("myCritter");
            critter.meals -= 1;
            Random rand = new Random();
            int like = rand.Next(0,100);
            if (like < 25) {
                HttpContext.Session.SetString("message", $"You fed your Dojodachi, but it was already full... -1 Meal, Fullness unchanged.");
            } else {
                int fullnessChange = rand.Next(5,11);
                critter.fullness += fullnessChange;
                HttpContext.Session.SetString("message", $"You fed your Dojodachi! -1 Meal, +{fullnessChange} Fullness.");
            }
            HttpContext.Session.SetObjectAsJson("myCritter", critter);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("play")]
        public IActionResult Play(){
            Dojodachi critter = HttpContext.Session.GetObjectFromJson<Dojodachi>("myCritter");
            critter.energy -= 5;
            if (critter.energy < 0) {
                critter.energy = 0;
            }
            Random rand = new Random();
            int like = rand.Next(0,100);
            if (like < 25) {
                HttpContext.Session.SetString("message", $"You played with your Dojodachi, but it wasn't in the mood... -5 Energy, Happiness unchanged.");
            } else {
                int happinessChange = rand.Next(5,11);
                critter.happiness += happinessChange;
                HttpContext.Session.SetString("message", $"You played with your Dojodachi! -5 Energy, +{happinessChange} Happiness.");
            }
            HttpContext.Session.SetObjectAsJson("myCritter", critter);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("work")]
        public IActionResult Work(){
            Dojodachi critter = HttpContext.Session.GetObjectFromJson<Dojodachi>("myCritter");
            critter.energy -= 5;
            if (critter.energy < 0) {
                critter.energy = 0;
            }
            Random rand = new Random();
            int newMeals = rand.Next(1,4);
            critter.meals += newMeals;
            HttpContext.Session.SetString("message", $"You sent your Dojodachi to work. -5 Energy, +{newMeals} Meals.");
            HttpContext.Session.SetObjectAsJson("myCritter", critter);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("sleep")]
        public IActionResult Sleep(){
            Dojodachi critter = HttpContext.Session.GetObjectFromJson<Dojodachi>("myCritter");
            critter.energy += 15;
            critter.fullness -= 5;
            critter.happiness -= 5;
            if (critter.fullness < 0) {
                critter.fullness = 0;
            }
            if (critter.happiness < 0) {
                critter.happiness = 0;
            }
            HttpContext.Session.SetObjectAsJson("myCritter", critter);
            HttpContext.Session.SetString("message", $"Your Dojodachi went to sleep! +15 Energy, -5 Fullness, -5 Happiness.");
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("restart")]
        public IActionResult Restart(){
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

    }
}