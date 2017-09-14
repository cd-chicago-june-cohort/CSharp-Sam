using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ValidForm.Models;
using System.Collections;

namespace ValidForm.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            List<string> errors = new List<string>();
            if (HttpContext.Session.GetString("first") == null){
                ViewBag.first = "";
            } else {
                ViewBag.first = HttpContext.Session.GetString("first");
            }
            if (HttpContext.Session.GetString("last") == null){
                ViewBag.last = "";
            } else {
                ViewBag.last = HttpContext.Session.GetString("last");
            }
            if (HttpContext.Session.GetString("email") == null){
                ViewBag.email = "";
            } else {
                ViewBag.email = HttpContext.Session.GetString("email");
            }
            if (HttpContext.Session.GetString("age") == null){
                ViewBag.age = "";
            } else {
                ViewBag.age = HttpContext.Session.GetString("age");
            }
            if (HttpContext.Session.GetString("pw") == null){
                ViewBag.pw = "";
            } else {
                ViewBag.pw = HttpContext.Session.GetString("pw");
            }

            return View();
        }

        [HttpPost]
        [Route("submit")]
        public IActionResult submit(string first, string last, string email, string age, string password){
            int count = 0;
            List<string> errorList = new List<string>();
            User newUser = new User(first,last,email,age,password);
            TryValidateModel(newUser);
            foreach(var error in ModelState.Values)
            {
                count += error.Errors.Count;
                if(error.Errors.Count > 0)
                {
                    errorList.Add(error.Errors[0].ErrorMessage);
                }
            }   
            foreach(var e in errorList){
                if (e.Contains("LastName")){
                    HttpContext.Session.SetString("last", e);
                }
                if (e.Contains("FirstName")){
                    HttpContext.Session.SetString("first", e);
                }
                if (e.Contains("Email")){
                    HttpContext.Session.SetString("email", e);
                }
                if (e.Contains("Age")){
                    HttpContext.Session.SetString("age", e);
                }
                if (e.Contains("Password")){
                    HttpContext.Session.SetString("pw", e);
                }
            }
            if (count > 0) {
                ViewBag.errors = errorList;
                return RedirectToAction("Index");
            } else {
                return RedirectToAction("Success");
            }
        }

        [HttpGet]
        [Route("success")]
        public IActionResult Success()
        {
            HttpContext.Session.Clear();
            return View("success");
        }

    }
}
