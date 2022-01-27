using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
            list = db.CartItems.GroupBy(item => item.Product.ProductId).OrderByDescending(item => item.Count()).Select(x=>x.Key).ToList().GetRange(0,10);
            ViewBag.BestProducts = db.Products.Where(x => list.Contains(x.ProductId));
            ViewData["bestselers"] = db.Products.Where(x => list.Contains(x.ProductId));
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

        // public ActionResult ProcessRequest()
        // {
        //     return View();
        // }
        //
        [HttpPost]  
        public ActionResult Contact(string customerName, string customerEmail, string customerRequest) {  
            try {  
                if (ModelState.IsValid) {  
                    var senderEmail = new MailAddress("happydogshop2022@gmail.com");
                    var password = "oguvqpleisdmpgnu";  
                    var sub = "Formularz kontaktowy od - " + customerName + " (" + customerEmail + ") ";  
                    var body = customerRequest;  
                    var smtp = new SmtpClient {  
                        Host = "smtp.gmail.com",  
                        Port = 587,  
                        EnableSsl = true,  
                        DeliveryMethod = SmtpDeliveryMethod.Network,  
                        UseDefaultCredentials = false,  
                        Credentials = new NetworkCredential(senderEmail.Address, password)  
                    };  
                    using(var mess = new MailMessage(senderEmail, senderEmail) {  
                        Subject = sub,  
                        Body = body  
                    }) {  
                        smtp.Send(mess);  
                    }  
                    return View();  
                }  
            } catch (Exception) {  
                ViewBag.Error = "Some Error";  
            }  
            return View();  
        }  
    }
}