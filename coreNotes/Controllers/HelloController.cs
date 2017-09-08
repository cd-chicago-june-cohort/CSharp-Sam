using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
 
namespace YourNamespace.Controllers
{
    public class HelloController : Controller
    {
        [HttpGet]
        [Route("index")]
        public string Index()
        {
            return "Hello World!";
        }

        [HttpGet]
        [Route("index/{Name}")]
        public string Method(string Name)
        {
            return $"Hello, {Name}"; 
        }

        [HttpGet]
        [Route("displayint")]
        public JsonResult DisplayInt()
        {
            var AnonObject = new {
                         FirstName = "Raz",
                         LastName = "Aquato",
                         Age = 10
                     };
            return Json(AnonObject);
        }

        [HttpGet]
        [Route("{first}/{last}/{age}/{color}")]
        public JsonResult CallingCard(string first, string last, int age, string color)
        {
            var User = new {
                         FirstName = first,
                         LastName = last,
                         Age = age,
                         Color = color
                     };
            return Json(User);
        }
 
    }
}