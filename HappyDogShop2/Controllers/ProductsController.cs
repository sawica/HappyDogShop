using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing.Printing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HappyDogShop2.Models;

namespace HappyDogShop2.Controllers
{
    public class ProductsController : Controller
    {
        private MyDbContext db = new MyDbContext();

        
        
        // for users:
        
        public ActionResult UserIndex(int categoryId = -1)
        {
            ViewData["categories"] = from category in db.Categories select category;
            ViewData["media"] = from media in db.MediaTypes select media;
            Console.WriteLine(categoryId);
            List<Product> list;
            if (categoryId != -1)
            {
                list = db.Products.Where(product => product.CategoryId == categoryId).ToList();
            }
            else
            {
                list = db.Products.ToList();
            }
            return View(list);
        }

        // GET: Products/Details/5
        public ActionResult UserDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        // GET: Products
        public ActionResult AdminIndex(int categoryId = -1, string Name = "", int Price1 = -1, int Price2 = -1, bool Sale = false)
        {
            List<Product> list = db.Products.ToList();
            
            if (categoryId != -1) list = list.Where(product => product.CategoryId == categoryId).ToList();
            
            if (Name != "") list = list.Where(product => product.Name == Name).ToList();
            
            if (Price1 != -1 & Price2 != -1) list = list.Where(product => product.Price >= Price1 && product.Price <= Price2).ToList();
            
            if (Sale == true) list = list.Where(product => product.Sale != null).ToList();

            return View(list);
        }

        // GET: Products/Details/5
        public ActionResult AdminDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult AdminCreate()
        {
            return View();
        }

        // POST: Products/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdminCreate([Bind(Include = "ProductId,Name,Description,Price,IsHidden,StockCount,MediaTypeId,CategoryId")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("AdminIndex");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult AdminEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdminEdit([Bind(Include = "ProductId,Name,Description,Price,IsHidden,StockCount,MediaTypeId,CategoryId")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("AdminIndex");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult AdminDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("AdminIndex");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        
        public ActionResult _ProductLayout(string searchString = "")
        {
            List<Category> list = new List<Category>();
            if (searchString != "")
            {
                foreach (var category1 in db.Categories.Where(category => category.Name == searchString).ToList()) list.Add(category1);
                foreach (var category1 in db.Categories.Where(category => category.Parent.Name == searchString).ToList()) list.Add(category1);
            }
            else
            {
                list = db.Categories.ToList();
            }
            return View(list);
        }
    }
}
