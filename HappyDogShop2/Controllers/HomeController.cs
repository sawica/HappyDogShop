using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HappyDogShop2.Models;

namespace HappyDogShop2.Controllers
{
    public class HomeController : Controller
    {
        private MyDbContext db = new MyDbContext();

        public ActionResult Index()
        {
            List<int> list = new List<int>();
            list = db.CartItems.GroupBy(item => item.Product.ProductId).OrderByDescending(item => item.Count()).Select(x=>x.Key).ToList().GetRange(0,3);
            ViewBag.BestProducts = db.Products.Where(x => list.Contains(x.ProductId));
            Console.Write(list.Count);
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}