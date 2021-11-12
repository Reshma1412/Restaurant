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
    public class RestaurantMenuItemsController : Controller
    {
        private RestaurantEntities1 db = new RestaurantEntities1();

        // GET: RestaurantMenuItems
        public ActionResult Index()
        {
            var restaurantMenuItems = db.RestaurantMenuItems.Include(r => r.Cuisine);
            return View(restaurantMenuItems.ToList());
        }

        // GET: RestaurantMenuItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RestaurantMenuItem restaurantMenuItem = db.RestaurantMenuItems.Find(id);
            if (restaurantMenuItem == null)
            {
                return HttpNotFound();
            }
            return View(restaurantMenuItem);
        }

        // GET: RestaurantMenuItems/Create
        public ActionResult Create()
        {
            ViewBag.CuisineID = new SelectList(db.Cuisines, "CuisineID", "CuisineName");
            return View();
        }

        // POST: RestaurantMenuItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MenuItemID,CuisineID,ItemName,ItemPrice")] RestaurantMenuItem restaurantMenuItem)
        {
            if (ModelState.IsValid)
            {
                db.RestaurantMenuItems.Add(restaurantMenuItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CuisineID = new SelectList(db.Cuisines, "CuisineID", "CuisineName", restaurantMenuItem.CuisineID);
            return View(restaurantMenuItem);
        }

        // GET: RestaurantMenuItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RestaurantMenuItem restaurantMenuItem = db.RestaurantMenuItems.Find(id);
            if (restaurantMenuItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.CuisineID = new SelectList(db.Cuisines, "CuisineID", "CuisineName", restaurantMenuItem.CuisineID);
            return View(restaurantMenuItem);
        }

        // POST: RestaurantMenuItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MenuItemID,CuisineID,ItemName,ItemPrice")] RestaurantMenuItem restaurantMenuItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(restaurantMenuItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CuisineID = new SelectList(db.Cuisines, "CuisineID", "CuisineName", restaurantMenuItem.CuisineID);
            return View(restaurantMenuItem);
        }

        // GET: RestaurantMenuItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RestaurantMenuItem restaurantMenuItem = db.RestaurantMenuItems.Find(id);
            if (restaurantMenuItem == null)
            {
                return HttpNotFound();
            }
            return View(restaurantMenuItem);
        }

        // POST: RestaurantMenuItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RestaurantMenuItem restaurantMenuItem = db.RestaurantMenuItems.Find(id);
            db.RestaurantMenuItems.Remove(restaurantMenuItem);
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
