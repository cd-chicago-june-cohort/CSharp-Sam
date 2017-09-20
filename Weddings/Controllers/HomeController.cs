using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Linq;
using Weddings.Models;
using Microsoft.EntityFrameworkCore;


namespace Weddings.Controllers
{
    public class HomeController : Controller
    {
        private WeddingContext _context;
 
        public HomeController(WeddingContext context)
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
                return RedirectToAction("Index");          
            }
            @ViewBag.errors = ModelState.Values;
            return View("Index");
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
            return View("Index");
        }

        [HttpGet]
        [Route("dashboard")]
        public IActionResult LoadDash()
        {
            if (HttpContext.Session.GetObjectFromJson<List<object>>("currentUser") == null){
                return RedirectToAction("Index");
            }
            List<Wedding> AllWeddings = _context.weddings.ToList();
            DateTime myDate = DateTime.Now;
            foreach(Wedding item in AllWeddings){
                int test = DateTime.Compare(item.date, myDate);
                if (test < 0) {
                    _context.weddings.Remove(item);
                    _context.SaveChanges(); 
                }
            }
            ViewBag.weddings = _context.weddings.Include(wedding=>wedding.guests).OrderBy(wedding => wedding.date).ToList();
            int myId = (int)HttpContext.Session.GetInt32("userId");
            ViewBag.userCheck = myId;
            User RetrievedUser = _context.users.SingleOrDefault(user => user.id == myId); 
            ViewBag.currentUser = RetrievedUser;
            
            return View("Dashboard");
        }

        [HttpGet]
        [Route("logout")]
        public IActionResult Logout(){
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("form")]
        public IActionResult WeddingForm(){
            if (HttpContext.Session.GetObjectFromJson<List<object>>("currentUser") == null){
                return RedirectToAction("Index");
            }
            ViewBag.min = DateTime.Now;
            return View("Form");
        }

        [HttpPost]
        [Route("create")]
        public IActionResult CreateWedding(WeddingViewModel model){
            int myId = (int)HttpContext.Session.GetInt32("userId");
            if(ModelState.IsValid)
            {
                Wedding NewWedding = new Wedding
                {
                    wedder1 = model.wedder1,
                    wedder2 = model.wedder2,
                    date = model.date,
                    address = model.address,
                    userId = myId
                };
                _context.Add(NewWedding);
                _context.SaveChanges();
                return RedirectToAction("LoadDash");          
            }
            @ViewBag.errors = ModelState.Values;
            return View("Form");
        }

        [HttpGet]
        [Route("delete/{myId}")]
        public IActionResult DeleteWedding(int myId){
            Wedding RetrievedWedding= _context.weddings.SingleOrDefault(wedding => wedding.id == myId);
            _context.weddings.Remove(RetrievedWedding);
            _context.SaveChanges();
            return RedirectToAction("LoadDash");  
        }

        [HttpGet]
        [Route("rsvp/{wedId}")]
        public IActionResult RSVP(int wedId){
            int guestId = (int)HttpContext.Session.GetInt32("userId");
            Guest NewGuest = new Guest
                {
                    userId = guestId,
                    weddingId = wedId
                };
                _context.Add(NewGuest);
                _context.SaveChanges();
            return RedirectToAction("LoadDash");  
        }

        [HttpGet]
        [Route("cancel/{wedId}")]
        public IActionResult Cancel(int wedId){
            int userId = (int)HttpContext.Session.GetInt32("userId");
            Guest CancellingGuest = _context.guests.SingleOrDefault(guest => guest.weddingId == wedId && guest.userId == userId);
            _context.guests.Remove(CancellingGuest);
            _context.SaveChanges();
            return RedirectToAction("LoadDash");  
        }
        
        [HttpGet]
        [Route("wedding/{wedId}")]
        public IActionResult GrabWedding(int wedId){
            Wedding RetrievedWedding = _context.weddings.Include(wedding=>wedding.guests).ThenInclude(guest=> guest.user).SingleOrDefault(wedding => wedding.id == wedId);
            @ViewBag.wedding = RetrievedWedding;
            return View("Wedding");
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
