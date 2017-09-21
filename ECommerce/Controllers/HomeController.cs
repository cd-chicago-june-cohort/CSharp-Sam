using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Linq;
using ECommerce.Models;
using Microsoft.EntityFrameworkCore;


namespace ECommerce.Controllers
{
    public class HomeController : Controller
    {
        private ECommerceContext _context;
 
        public HomeController(ECommerceContext context)
        {
            _context = context;
        }

        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Dashboard()
        {
            List<Customer> AllCustomers = _context.customers.ToList();
            ViewBag.customers = AllCustomers;
            List<Product> AllProducts = _context.products.Where(p=> p.quantity > 0).ToList();
            ViewBag.products = AllProducts;
            List<Purchase> AllOrders = _context.purchases.Where(p => p.quantity > 0).Include(p => p.customer).Include(p => p.product).ToList();
            ViewBag.orders = AllOrders;
            return View();
        }

        [HttpGet]
        [Route("products")]
        public IActionResult Products()
        {
            List<Product> AllProducts = _context.products.Where(p => p.quantity > 0).ToList();
            ViewBag.products = AllProducts;
            return View();
        }

        [HttpGet]
        [Route("orders")]
        public IActionResult Orders()
        {
            List<Customer> AllCustomers = _context.customers.ToList();
            ViewBag.customers = AllCustomers;
            List<Product> AllProducts = _context.products.Where(p=> p.quantity > 0).ToList();
            ViewBag.products = AllProducts;
            List<Purchase> AllOrders = _context.purchases.Where(p => p.quantity > 0).Include(p => p.customer).Include(p => p.product).ToList();
            ViewBag.orders = AllOrders;
            return View();
        }

        [HttpGet]
        [Route("customers")]
        public IActionResult Customers()
        {
            List<Customer> AllCustomers = _context.customers.ToList();
            ViewBag.customers = AllCustomers;
            return View();
        }

        [HttpPost]
        [Route("add/customer")]
        public IActionResult CreateCustomer(RegisterViewModel model){
            List<Customer> AllCustomers = _context.customers.ToList();
            @ViewBag.customers = AllCustomers;
            List<Customer> CustomerMatch = _context.customers.Where(customer => customer.name == model.name).ToList();
            if(CustomerMatch.Count > 0){
                @ViewBag.existing = "This customer already exists!";
                return View("Customers");
            }
            if (ModelState.IsValid){
                Customer newCustomer = new Customer{
                    name = model.name
                };
                _context.Add(newCustomer);
                _context.SaveChanges();
                return RedirectToAction("Customers");
            } else {
                return View("Customers");
            }
        }

        [HttpGet]
        [Route("customers/remove/{myId}")]
        public IActionResult DeleteCustomer(int myId){
            Customer thisCustomer = _context.customers.SingleOrDefault(c => c.id == myId);
            _context.Remove(thisCustomer);
            _context.SaveChanges();
            return RedirectToAction("Customers");
        }


        [HttpPost]
        [Route("search")]
        public IActionResult search(string searchStr){
            List<Customer> myGuys = _context.customers.Where(c => c.name.ToLower().Contains(searchStr.ToLower())).ToList();
            @ViewBag.customers = myGuys;
            return View("Customers");
        }

        [HttpPost]
        [Route("add/product")]
        public IActionResult CreateProduct(ProductViewModel model){
            List<Product> AllProducts = _context.products.Where(p => p.quantity > 0).ToList();
            ViewBag.products = AllProducts;
            List<Product> ProductMatch = _context.products.Where(product => product.name == model.name).ToList();
            if(ProductMatch.Count > 0){
                @ViewBag.existing = "This product already exists!";
                return View("Products");
            }
            if (ModelState.IsValid){
                Product newProduct = new Product{
                    name = model.name,
                    image = model.image,
                    description = model.description,
                    quantity = model.quantity
                };
                _context.Add(newProduct);
                _context.SaveChanges();
                return RedirectToAction("Products");
            } else {
                return View("Products");
            }
        }

        [HttpPost]
        [Route("filterProducts")]
        public IActionResult filterProducts(string searchStr){
            Console.WriteLine("NOTICE ME");
            Console.WriteLine(searchStr);
            List<Product> myGuys = _context.products.Where(product => product.name.ToLower().Contains(searchStr.ToLower())).ToList();
            @ViewBag.products = myGuys;
            return View("Products");
        }

        [HttpPost]
        [Route("add/order")]
        public IActionResult CreateOrder(PurchaseViewModel model){
            List<Customer> AllCustomers = _context.customers.ToList();
            ViewBag.customers = AllCustomers;
            List<Product> AllProducts = _context.products.Where(p=> p.quantity > 0).ToList();
            ViewBag.products = AllProducts;
            List<Purchase> AllOrders = _context.purchases.Where(p => p.quantity > 0).Include(p => p.customer).Include(p => p.product).ToList();
            ViewBag.orders = AllOrders;
            if (ModelState.IsValid){
                Purchase newOrder = new Purchase{
                    quantity = model.quantity,
                    customerId = model.customerId,
                    productId = model.productId
                };
                _context.Add(newOrder);
                Product myProduct = _context.products.SingleOrDefault(p => p.id == model.productId);
                myProduct.quantity -= model.quantity;
                _context.SaveChanges();
                return RedirectToAction("Orders");
            } else {
                return View("Orders");
            }
        }
        [HttpPost]
        [Route("filterOrders")]
        public IActionResult filterOrders(string searchStr){
            Console.WriteLine(searchStr);
            List<Purchase> myGuys = _context.purchases.Include(p => p.customer).Include(p => p.product).Where(p => p.customer.name.ToLower().Contains(searchStr.ToLower()) || p.product.name.ToLower().Contains(searchStr.ToLower()) ).ToList();
            @ViewBag.orders = myGuys;
            List<Customer> AllCustomers = _context.customers.ToList();
            ViewBag.customers = AllCustomers;
            List<Product> AllProducts = _context.products.Where(p=> p.quantity > 0).ToList();
            ViewBag.products = AllProducts;
            return View("Orders");
        }

        [HttpPost]
        [Route("dashSearch")]
        public IActionResult dashSearch(string searchStr){
            Console.WriteLine(searchStr);
            List<Purchase> myGuys = _context.purchases.Include(p => p.customer).Include(p => p.product).Where(p => p.customer.name.ToLower().Contains(searchStr.ToLower()) || p.product.name.ToLower().Contains(searchStr.ToLower()) ).ToList();
            ViewBag.orders = myGuys;
            List<Customer> AllCustomers = _context.customers.Where(c => c.name.ToLower().Contains(searchStr.ToLower())).ToList();
            ViewBag.customers = AllCustomers;
            List<Product> AllProducts = _context.products.Where(pro => pro.name.ToLower().Contains(searchStr.ToLower())).ToList();
            ViewBag.products = AllProducts;
            return View("Dashboard");
        }

    }
}
