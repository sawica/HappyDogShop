using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using HappyDogShop2.Models;

namespace HappyDogShop2.Controllers
{
    public class OrderController : Controller
    {
       private MyDbContext db = new MyDbContext();

        // GET: Orders
        public ActionResult Index(int UserId = -1)
        {
            List<Order> list= new List<Order>();
            if (UserId != -1)
            {
                list = db.Orders.Where(order => order.UserId == UserId).ToList();
            }
            else
            {
                list = db.Orders.ToList();
            }
            return View(list);
        }
        
        public ActionResult UserIndex(int UserId)
        {
            List<Order> list= new List<Order>();
            list = db.Orders.Where(order => order.UserId == UserId).ToList();
            foreach (var order in list)
            {
                ViewData[order.OrderId.ToString()] = order.CartItems;
            }

            ViewBag.count = 0;
            return View(list);
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            prepareList();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewData["cartItems"] = db.CartItems.Where(item => item.Order.OrderId == order.OrderId).ToList();
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
        
            return View();
        }

        // POST: Orders/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderId,UserId,AmountPaid, Status")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            Session["orderId"] = order.OrderId;
            return View(order);
        }
        
        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderId,UserId,AmountPaid, Status")] Order order)
        {
            if (ModelState.IsValid)
            {
                if (Status.Otwarte.ToString().Equals(Request.Form["wybrany"])) order.Status = Status.Otwarte;
                if (Status.Zapłacone.ToString().Equals(Request.Form["wybrany"])) order.Status = Status.Zapłacone;
                if (Status.Dostarczone.ToString().Equals(Request.Form["wybrany"])) order.Status = Status.Dostarczone;
                if (Status.Przyjęte.ToString().Equals(Request.Form["wybrany"]))  order.Status = Status.Przyjęte;
                if (Status.Wysłane.ToString().Equals(Request.Form["wybrany"])) order.Status = Status.Wysłane;
                
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult OrderAgain(int? NewOrderId, int OldOrderId)
        {
            if (NewOrderId == null)
            {
                Order order = new Order();
                Console.Write((int) Session["userId"]);
                    order.UserId = (int) Session["userId"];
                    order.Date = DateTime.Now;
                    db.Orders.Add(order);
                    db.SaveChanges();                    
                    Session["orderId"] = order.OrderId;
                
            }

            foreach (var item in db.CartItems.Where(i=>i.OrderId == OldOrderId))
            {
                CartItem cartItem = new CartItem();
                cartItem.Quantity = item.Quantity;
                cartItem.ProductId = item.ProductId;
                cartItem.OrderId = (int) Session["orderId"];
                db.CartItems.Add(cartItem);
            }
            
            db.SaveChanges();
            return RedirectToAction("UserIndex", "CartItem", new { OrderId=(int) Session["orderId"]} );
        }
        
        public ActionResult PlaceAnOrder(int OrderId, decimal AmountPaid)
        {
            Order order = db.Orders.Find(OrderId);
            Console.Write(order.UserId);
            order.AmountPaid = AmountPaid;
            order.Status = Status.Przyjęte;

            List<CartItem> itemList = new List<CartItem>();
            itemList = db.CartItems.Where(item => item.Order.OrderId == order.OrderId).ToList();
            foreach (var item in itemList) db.Products.Where(product => product.ProductId == item.ProductId).ToList().ForEach(pr => pr.StockCount = pr.StockCount - item.Quantity);
            
            db.Entry(order).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("UserIndex", new {UserId = order.UserId});
        }
        public void prepareList()
        {
            ViewData["media"] = from media in db.MediaTypes select media;
            List<SelectListItem> productList = new List<SelectListItem>();
            List<SelectListItem> orderList = new List<SelectListItem>();
            List<SelectListItem> mediaList = new List<SelectListItem>();
            
            foreach (var product in db.Products) productList.Add(new SelectListItem{ Text = product.Name, Value = product.ProductId.ToString()});
            foreach (var order in db.Orders) orderList.Add(new SelectListItem{ Text = order.OrderId.ToString(), Value = order.OrderId.ToString()});
            foreach (var media in db.MediaTypes) mediaList.Add(new SelectListItem{ Text = media.Title, Value = media.MediaTypeId.ToString()});
            
            ViewData["productList"] = productList;
            ViewData["orderList"] = orderList;
            ViewData["mediaList"] = mediaList;
        }
    }
}