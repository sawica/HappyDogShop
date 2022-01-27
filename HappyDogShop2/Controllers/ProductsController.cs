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
using PagedList;

namespace HappyDogShop2.Controllers
{
    public class ProductsController : Controller
    {
        private MyDbContext db = new MyDbContext();

        
        
        // for users:
        
        public ActionResult UserIndex(int? page, int categoryId = -1 )
        {
            ViewData["categories"] = from category in db.Categories select category;
            ViewData["media"] = from media in db.MediaTypes select media;
            ViewData["saleList"] = from sale in db.Sales select sale;
            Console.WriteLine(categoryId);
            List<Product> list;
            if (categoryId == -2) //nowości
            {
                list = db.Products.OrderByDescending(product => product.ProductId).Take(10).ToList();
            }
            else if (categoryId == -3) //promocje
            {
                // list = ((db.Products.Where(product => product.SaleId != null).ToList()).Where(product => product.Sale.StartDate <= @DateTime.Now).ToList()).Where(product => product.Sale.EndDate >= @DateTime.Now).ToList();

                list = db.Products.Where(product => product.SaleId != null && product.Sale.StartDate <= @DateTime.Now &&
                                                    product.Sale.EndDate >= @DateTime.Now).ToList();

            }
            else if (categoryId != -1) 
            {
                list = db.Products.Where(product => product.CategoryId == categoryId).ToList();
                Console.WriteLine("poszla kategoria");
            }
            else //wszystko
            {
                list = db.Products.ToList();
            }
            int pageSize = 6;
            int pageNumber = (page ?? 1);
            return View(list.ToPagedList(pageNumber, pageSize));
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
        public ActionResult AdminIndex(int categoryId = -1, string categoryName ="", string Name = "", int Price1 = -1, int Price2 = -1, bool Sale = false, bool inStock = true)
        {
            List<Product> list = db.Products.ToList();
            ViewBag.min = db.Products.Min(product => product.Price);
            ViewBag.max = db.Products.Max(product => product.Price);

            if (categoryId != -1) list = list.Where(product => product.CategoryId == categoryId).ToList();
            
            if (categoryName != "") list = list.Where(product => product.Category.Name == categoryName).ToList();

            if (Name != "") list = list.Where(product => product.Name == Name).ToList();
            
            if (Price1 != -1 & Price2 != -1) list = list.Where(product => product.Price >= Price1 && product.Price <= Price2).ToList();
            
            if (Sale == true) list = list.Where(product => product.Sale != null).ToList();
            
            if (inStock == false) list = list.Where(product => product.StockCount == 0).ToList();

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
            prepareList();

            return View();
        }

        // POST: Products/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdminCreate([Bind(Include = "ProductId,Name,Description,Price,IsHidden,StockCount,MediaTypeId,CategoryId, SaleId")] Product product)
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
            prepareList();
            
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
        public ActionResult AdminEdit([Bind(Include = "ProductId,Name,Description,Price,IsHidden,StockCount,MediaTypeId,CategoryId, SaleId")] Product product)
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

        public void prepareList()
        {
            ViewData["categories"] = from category in db.Categories select category;
            ViewData["media"] = from media in db.MediaTypes select media;
            
            List<SelectListItem> categoryList = new List<SelectListItem>();
            List<SelectListItem> mediaList = new List<SelectListItem>();
            List<SelectListItem> saleList = new List<SelectListItem>();

            foreach (var category in db.Categories) categoryList.Add(new SelectListItem{ Text = category.Name, Value = category.CategoryId.ToString()});
            foreach (var sale in db.Sales) saleList.Add(new SelectListItem{ Text = sale.Name + ": " + sale.ValueInPercent + "%", Value = sale.SaleId.ToString()});
            foreach (var media in db.MediaTypes) mediaList.Add(new SelectListItem{ Text = media.Title, Value = media.MediaTypeId.ToString()});

            ViewData["categoriesList"] = categoryList;
            ViewData["mediaList"] = mediaList;
            ViewData["saleList"] = saleList;
        }
    }
}
