using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RestaurantMVCWithEF.Models;

namespace RestaurantMVCWithEF.Controllers
{
    public class DiningTableTracksController : Controller
    {
        private RestaurantEntities1 db = new RestaurantEntities1();

        // GET: DiningTableTracks
        public ActionResult Index()
        {
            var diningTableTracks = db.DiningTableTracks.Include(d => d.DiningTable);
            return View(diningTableTracks.ToList());
        }

        // GET: DiningTableTracks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiningTableTrack diningTableTrack = db.DiningTableTracks.Find(id);
            if (diningTableTrack == null)
            {
                return HttpNotFound();
            }
            return View(diningTableTrack);
        }

        // GET: DiningTableTracks/Create
        public ActionResult Create()
        {
            ViewBag.DiningTableID = new SelectList(db.DiningTables, "DiningTableID", "DiningLocation");
            return View();
        }

        // POST: DiningTableTracks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DiningTableTrackID,DiningTableID,TableStatus")] DiningTableTrack diningTableTrack)
        {
            if (ModelState.IsValid)
            {
                db.DiningTableTracks.Add(diningTableTrack);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DiningTableID = new SelectList(db.DiningTables, "DiningTableID", "DiningLocation", diningTableTrack.DiningTableID);
            return View(diningTableTrack);
        }

        // GET: DiningTableTracks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiningTableTrack diningTableTrack = db.DiningTableTracks.Find(id);
            if (diningTableTrack == null)
            {
                return HttpNotFound();
            }
            ViewBag.DiningTableID = new SelectList(db.DiningTables, "DiningTableID", "DiningLocation", diningTableTrack.DiningTableID);
            return View(diningTableTrack);
        }

        // POST: DiningTableTracks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DiningTableTrackID,DiningTableID,TableStatus")] DiningTableTrack diningTableTrack)
        {
            if (ModelState.IsValid)
            {
                db.Entry(diningTableTrack).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DiningTableID = new SelectList(db.DiningTables, "DiningTableID", "DiningLocation", diningTableTrack.DiningTableID);
            return View(diningTableTrack);
        }

        // GET: DiningTableTracks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiningTableTrack diningTableTrack = db.DiningTableTracks.Find(id);
            if (diningTableTrack == null)
            {
                return HttpNotFound();
            }
            return View(diningTableTrack);
        }

        // POST: DiningTableTracks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DiningTableTrack diningTableTrack = db.DiningTableTracks.Find(id);
            db.DiningTableTracks.Remove(diningTableTrack);
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
