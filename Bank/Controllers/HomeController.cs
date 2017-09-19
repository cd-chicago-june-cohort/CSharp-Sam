using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Bank.Models;
using System.Linq;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace Bank.Controllers
{
    public class HomeController : Controller
    {

        private BankContext _context;
 
        public HomeController(BankContext context)
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

        [HttpPost]
        [Route("register")]
        public IActionResult Register(RegisterViewModel model)
        {
            List<User> UserMatch = _context.users.Where(user => user.email == model.email).ToList();
            if (UserMatch.Count != 0){
                ViewBag.existing = "This email is already registered. Please log in or use a different email.";
                Console.WriteLine(ViewBag.existing);
                return View("Index");
            }

            if(ModelState.IsValid)
            {
                User NewUser = new User
                {
                    first = model.first,
                    last = model.last,
                    email = model.email,
                    password = model.password
                };
                _context.Add(NewUser);
                _context.SaveChanges();
                return RedirectToAction("Login");          
            }
            Console.WriteLine("alleged errors");
            Console.WriteLine(ModelState.Values);
            foreach(var error in ModelState.Values){
                if(error.Errors.Count > 0){
                    Console.WriteLine(error.Errors[0].ErrorMessage);
                }
            }
            @ViewBag.errors = ModelState.Values;
            return View("Index");
        }

        [HttpGet]
        [Route("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("process")]
        public IActionResult Process(string email, string password)
        {
            List<User> UserMatch = _context.users.Where(user => user.email == email).ToList();
            if (UserMatch.Count == 0) {
                @ViewBag.notExisting = "Email not registered. Please register before attempting login.";
            } else {
                var myUser = UserMatch[0];
                if (password == myUser.password){
                    HttpContext.Session.SetObjectAsJson("currentUser", UserMatch);
                    HttpContext.Session.SetInt32("userId", myUser.id);
                    return RedirectToAction("LoadDash");
                } else {
                    @ViewBag.incorrectPW = "Incorrect password. Please try again.";
                }
            }
            return View("Login");
        }

        [HttpGet]
        [Route("account")]
        public IActionResult LoadDash()
        {
            if (HttpContext.Session.GetString("FundMsg") != null){
                @ViewBag.FundMsg = HttpContext.Session.GetString("FundMsg");
            }
            if (HttpContext.Session.GetObjectFromJson<List<object>>("currentUser") == null){
                return RedirectToAction("Index");
            }
            int myId = (int)HttpContext.Session.GetInt32("userId");
            User RetrievedUser = _context.users.Include(user => user.transactions).SingleOrDefault(user => user.id == myId); 
            @ViewBag.currentUser = RetrievedUser;
            
            return View("Account");
        }

        [HttpPost]
        [Route("transact")]
        public IActionResult Transact(TransactionViewModel model){
            int myId = (int)HttpContext.Session.GetInt32("userId");
            User RetrievedUser = _context.users.SingleOrDefault(user => user.id == myId);      
            if (RetrievedUser.balance + model.amount < 0){
                HttpContext.Session.SetString("FundMsg", "Insufficient funds. Cannot withdraw.");
                return RedirectToAction("LoadDash");
            }      
            if(ModelState.IsValid){
                HttpContext.Session.SetString("FundMsg", "Success!");
                Transaction action = new Transaction{
                    amount = model.amount,
                    userId = myId
                };
                RetrievedUser.balance += model.amount;
                _context.Add(action);
                _context.SaveChanges();
                return RedirectToAction("LoadDash");
            }
            else 
            {
                @ViewBag.error = "Amount field is required";
                return RedirectToAction("LoadDash");
            }
        }

        [HttpGet]
        [Route("logout")]
        public IActionResult Logout(){
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }


    }

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

}
