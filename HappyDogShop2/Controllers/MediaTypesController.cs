using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HappyDogShop2.Models;

namespace HappyDogShop2.Controllers
{
    public class MediaTypesController: Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: MediaTypes
        public ActionResult Index(string searchString = "")
        {
            List<MediaType> list= new List<MediaType>();
            if (searchString != "")
            {
                list = db.MediaTypes.Where(mediaType => mediaType.Title == searchString).ToList();
            }
            else
            {
                list = db.MediaTypes.ToList();
            }
            return View(list);
        }

        // GET: MediaTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MediaType mediaType = db.MediaTypes.Find(id);
            if (mediaType == null)
            {
                return HttpNotFound();
            }
            return View(mediaType);
        }

        // GET: MediaTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MediaTypes/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SMediaTypeId,Title,ImagePath")] MediaType mediaType)
        {
            if (ModelState.IsValid)
            {
                db.MediaTypes.Add(mediaType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mediaType);
        }

        // GET: MediaTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MediaType mediaType = db.MediaTypes.Find(id);
            if (mediaType == null)
            {
                return HttpNotFound();
            }
            return View(mediaType);
        }

        // POST: MediaTypes/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MediaTypeId,Title,ImagePath")] MediaType mediaType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mediaType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mediaType);
        }

        // GET: MediaTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MediaType mediaType = db.MediaTypes.Find(id);
            if (mediaType == null)
            {
                return HttpNotFound();
            }
            return View(mediaType);
        }

        // POST: MediaTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MediaType mediaType = db.MediaTypes.Find(id);
            db.MediaTypes.Remove(mediaType);
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
    }
}