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
    public class OrderInfoesController : Controller
    {
        private RestaurantEntities1 db = new RestaurantEntities1();

        // GET: OrderInfoes
        public ActionResult Index()
        {
            var orderInfoes = db.OrderInfoes.Include(o => o.DiningTable).Include(o => o.RestaurantMenuItem).Include(o => o.RestaurantDetail);
            return View(orderInfoes.ToList());
        }

        // GET: OrderInfoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderInfo orderInfo = db.OrderInfoes.Find(id);
            if (orderInfo == null)
            {
                return HttpNotFound();
            }
            return View(orderInfo);
        }

        // GET: OrderInfoes/Create
        public ActionResult Create()
        {
            ViewBag.DiningTableID = new SelectList(db.DiningTables, "DiningTableID", "DiningLocation");
            ViewBag.MenuItemID = new SelectList(db.RestaurantMenuItems, "MenuItemID", "ItemName");
            ViewBag.RestuarantID = new SelectList(db.RestaurantDetails, "RestuarantID", "RestuarantName");
            return View();
        }

        // POST: OrderInfoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderID,OrderDate,RestuarantID,MenuItemID,ItemQuantity,OrderAmount,DiningTableID")] OrderInfo orderInfo)
        {
            if (ModelState.IsValid)
            {
                db.OrderInfoes.Add(orderInfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DiningTableID = new SelectList(db.DiningTables, "DiningTableID", "DiningLocation", orderInfo.DiningTableID);
            ViewBag.MenuItemID = new SelectList(db.RestaurantMenuItems, "MenuItemID", "ItemName", orderInfo.MenuItemID);
            ViewBag.RestuarantID = new SelectList(db.RestaurantDetails, "RestuarantID", "RestuarantName", orderInfo.RestuarantID);
            return View(orderInfo);
        }

        // GET: OrderInfoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderInfo orderInfo = db.OrderInfoes.Find(id);
            if (orderInfo == null)
            {
                return HttpNotFound();
            }
            ViewBag.DiningTableID = new SelectList(db.DiningTables, "DiningTableID", "DiningLocation", orderInfo.DiningTableID);
            ViewBag.MenuItemID = new SelectList(db.RestaurantMenuItems, "MenuItemID", "ItemName", orderInfo.MenuItemID);
            ViewBag.RestuarantID = new SelectList(db.RestaurantDetails, "RestuarantID", "RestuarantName", orderInfo.RestuarantID);
            return View(orderInfo);
        }

        // POST: OrderInfoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderID,OrderDate,RestuarantID,MenuItemID,ItemQuantity,OrderAmount,DiningTableID")] OrderInfo orderInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DiningTableID = new SelectList(db.DiningTables, "DiningTableID", "DiningLocation", orderInfo.DiningTableID);
            ViewBag.MenuItemID = new SelectList(db.RestaurantMenuItems, "MenuItemID", "ItemName", orderInfo.MenuItemID);
            ViewBag.RestuarantID = new SelectList(db.RestaurantDetails, "RestuarantID", "RestuarantName", orderInfo.RestuarantID);
            return View(orderInfo);
        }

        // GET: OrderInfoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderInfo orderInfo = db.OrderInfoes.Find(id);
            if (orderInfo == null)
            {
                return HttpNotFound();
            }
            return View(orderInfo);
        }

        // POST: OrderInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderInfo orderInfo = db.OrderInfoes.Find(id);
            db.OrderInfoes.Remove(orderInfo);
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
