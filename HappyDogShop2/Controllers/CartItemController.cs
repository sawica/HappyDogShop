using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using HappyDogShop2.Models;

namespace HappyDogShop2.Controllers
{
    public class CartItemController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: CartItems
        public ActionResult Index(int OrderId = -1)
        {
            List<CartItem> list= new List<CartItem>();
            if (OrderId != -1)
            {
                list = db.CartItems.Where(cartItem => cartItem.OrderId == OrderId).ToList();
            }
            else
            {
                list = db.CartItems.ToList();
            }
            return View(list);
        }

        // GET: CartItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartItem cartItem = db.CartItems.Find(id);
            if (cartItem == null)
            {
                return HttpNotFound();
            }
            return View(cartItem);
        }

        // GET: CartItems/Create
        public ActionResult Create()
        {
            prepareList();
            return View();
        }

        // POST: CartItems/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ItemId,Quantity,ProductId, OrderId")] CartItem cartItem)
        {
            //TODO jak zrobic to zeby przy braku otwartego zamowienia tworzylo nowe
            // if (cartItem.OrderId = null)
            // {
            //     Order order = new Order();
            //     Session["orderId"] = order.OrderId;
            // }
            if (ModelState.IsValid)
            {
                db.CartItems.Add(cartItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cartItem);
        }
        public ActionResult Create2(int Quantity, int ProductId, int? OrderId)
        {
            //TODO jak zrobic to zeby przy braku otwartego zamowienia tworzylo nowe
            
            if (OrderId == null)
            {
                if (Session["userId"] == null)
                {
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    Order order = new Order();
                    order.UserId = (int) Session["userId"];
                    order.Date = DateTime.Now;
                    Session["orderId"] = order.OrderId;
                    db.Orders.Add(order);
                    db.SaveChanges();
                }
            }

            CartItem cartItem = new CartItem();
            cartItem.Quantity = Quantity;
            cartItem.ProductId = ProductId;
            cartItem.OrderId = (int) Session["orderId"];
            db.CartItems.Add(cartItem);
            db.SaveChanges();
            return RedirectToAction("UserIndex");

        }

        // GET: CartItems/Edit/5
        public ActionResult Edit(int? id)
        {
            prepareList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartItem cartItem = db.CartItems.Find(id);
            if (cartItem == null)
            {
                return HttpNotFound();
            }
            return View(cartItem);
        }

        // POST: CartItems/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ItemId,Quantity,ProductId, OrderId")] CartItem cartItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cartItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cartItem);
        }

        // GET: CartItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartItem cartItem = db.CartItems.Find(id);
            if (cartItem == null)
            {
                return HttpNotFound();
            }
            return View(cartItem);
        }

        // POST: CartItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CartItem cartItem = db.CartItems.Find(id);
            db.CartItems.Remove(cartItem);
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

        public ActionResult UserIndex(int? OrderId = 0)
        {
            ViewData["categories"] = from category in db.Categories select category;
            ViewData["media"] = from media in db.MediaTypes select media;
            ViewData["products"] = from product in db.Products select product;
            ViewBag.orderId = OrderId;
            float sum = 0;
            
            List<CartItem> list= new List<CartItem>();
            
            if (OrderId > 0)
            {
                
                list = db.CartItems.Where(cartItem => cartItem.OrderId == OrderId).ToList();
            }
            foreach (CartItem item in list)
            {
                Product product = db.Products.Find(item.ProductId);
                sum += product.Price;
            }

            ViewBag.sum = sum;
            return View(list);
        }
        public void prepareList()
        {
            List<SelectListItem> productList = new List<SelectListItem>();
            List<SelectListItem> orderList = new List<SelectListItem>();

            foreach (var product in db.Products) productList.Add(new SelectListItem{ Text = product.Name, Value = product.ProductId.ToString()});
            foreach (var order in db.Orders) orderList.Add(new SelectListItem{ Text = order.OrderId.ToString(), Value = order.OrderId.ToString()});

            ViewData["productList"] = productList;
            ViewData["orderList"] = orderList;
        }
    }
}