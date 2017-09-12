using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DbConnection;

namespace QuotingDojo.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("quotes")]
        public IActionResult quotes()
        {
            List<Dictionary<string, object>> AllQuotes = DbConnector.Query("SELECT * FROM quotes ORDER BY created_at DESC");
            ViewBag.Quotes = AllQuotes;
            return View();
        }

        [HttpPost]
        [Route("add")]
        public IActionResult addQuote(string name, string content){
            DbConnector.Execute($"INSERT INTO quotes (speaker, content, created_at) VALUES ('{name}', '{content}', now())");
            return RedirectToAction("quotes");
        }

    }
}
