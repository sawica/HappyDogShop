using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using HappyDogShop2.Models;

namespace HappyDogShop2.Controllers
{
    private MyDbContext db = new MyDbContext();
    public class TrashController : Controller
    {
        // GET
        public ActionResult Index()
        {
            List<CartItem> list= new List<CartItem>();
            return View(list);

        }
        
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
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CartItem cartItem = db.CartItems.Find(id);
            db.CartItems.Remove(cartItem);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}