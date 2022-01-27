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

        public ActionResult OrderAgain(int id)
        {
            throw new System.NotImplementedException();
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
    }
}