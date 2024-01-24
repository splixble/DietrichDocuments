using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebSongs1.Models;

namespace WebSongs1.Controllers
{
    public class viewsongperformancetotalsController : Controller
    {
        private SongbookEntities db = new SongbookEntities();

        // GET: viewsongperformancetotals
        public ActionResult Index()
        {
            return View(db.viewsongperformancetotals.ToList());
        }

        // GET: viewsongperformancetotals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            viewsongperformancetotal viewsongperformancetotal = db.viewsongperformancetotals.Find(id);
            if (viewsongperformancetotal == null)
            {
                return HttpNotFound();
            }
            return View(viewsongperformancetotal);
        }

        // GET: viewsongperformancetotals/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: viewsongperformancetotals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Total,SongID,TitleAndArtist,firstPerformed,lastPerformed")] viewsongperformancetotal viewsongperformancetotal)
        {
            if (ModelState.IsValid)
            {
                db.viewsongperformancetotals.Add(viewsongperformancetotal);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(viewsongperformancetotal);
        }

        // GET: viewsongperformancetotals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            viewsongperformancetotal viewsongperformancetotal = db.viewsongperformancetotals.Find(id);
            if (viewsongperformancetotal == null)
            {
                return HttpNotFound();
            }
            return View(viewsongperformancetotal);
        }

        // POST: viewsongperformancetotals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Total,SongID,TitleAndArtist,firstPerformed,lastPerformed")] viewsongperformancetotal viewsongperformancetotal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(viewsongperformancetotal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(viewsongperformancetotal);
        }

        // GET: viewsongperformancetotals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            viewsongperformancetotal viewsongperformancetotal = db.viewsongperformancetotals.Find(id);
            if (viewsongperformancetotal == null)
            {
                return HttpNotFound();
            }
            return View(viewsongperformancetotal);
        }

        // POST: viewsongperformancetotals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            viewsongperformancetotal viewsongperformancetotal = db.viewsongperformancetotals.Find(id);
            db.viewsongperformancetotals.Remove(viewsongperformancetotal);
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
