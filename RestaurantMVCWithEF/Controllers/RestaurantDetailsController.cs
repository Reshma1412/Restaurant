using RestaurantMVCWithEF.Models;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Data.SqlClient;

namespace RestaurantMVCWithEF.Controllers
{
    public class RestaurantDetailsController : Controller
    {
         
        private RestaurantEntities1 db = new RestaurantEntities1();
        //private object hc;
        private RestaurantDetail rd = new RestaurantDetail();



        //Validation
        //[HttpPost]
        //public JsonResult ChkNoValidation(string MobileNo)
        //{
        //    //return Json(!db.RestaurantDetails.Any(x => x.MobileNo == MobNo), JsonRequestBehavior.AllowGet);
        //    var result = db.RestaurantDetails.Where(x => x.MobileNo == MobileNo).FirstOrDefault();
        //    var ifAvailable = true;
        //    if (result == null)
        //    {
        //        //Mobile No available
        //        ifAvailable = true;
        //    }
        //    else
        //    {
        //        // Mobile No not available
        //        ifAvailable = false;
        //    }

        //    return Json(ifAvailable);
        //}

        
        [HttpPost]
        public JsonResult ChkNoValidation(string MobileNo, int? RestuarantID)
        {
            var validateName = db.RestaurantDetails.FirstOrDefault
                                (x => x.MobileNo == MobileNo && x.RestuarantID != RestuarantID);
            if (validateName != null)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public JsonResult ChkNameValidation(string RestuarantName, int? RestuarantID)
        {
            var validateName = db.RestaurantDetails.FirstOrDefault
                                (x => x.RestuarantName == RestuarantName && x.RestuarantID != RestuarantID);
            if (validateName != null)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }

        //[HttpPost]
        //public JsonResult ChkNameValidation(string RestuarantName)
        //{
        //    //return Json(!db.RestaurantDetails.Any(x => x.MobileNo == MobNo), JsonRequestBehavior.AllowGet);
        //    var result = db.RestaurantDetails.Where(x => x.RestuarantName == RestuarantName).FirstOrDefault();
        //    var ifAvailable = true;
        //    if (result == null)
        //    {
        //        //Name available
        //        ifAvailable = true;
        //    }
        //    else
        //    {
        //        // Name not available
        //        ifAvailable = false;
        //    }

        //    return Json(ifAvailable);
        //}



        // GET: RestaurantDetails
        public ActionResult Index()
        {
            
            return View(db.RestaurantDetails.ToList());
        }

        // GET: RestaurantDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RestaurantDetail restaurantDetail = db.RestaurantDetails.Find(id);
            if (restaurantDetail == null)
            {
                return HttpNotFound();
            }
            return View(restaurantDetail);
        }

        // GET: RestaurantDetails/Create
        public ActionResult Create()
        {
            //return View(new RestaurantDetailViewModel());
            return View(new RestaurantDetail());
        }

        // POST: RestaurantDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "RestuarantID,RestuarantName,RestaurantAddress,MobileNo")] RestaurantDetailViewModel restaurantDetail)
        public ActionResult Create([Bind(Include = "RestuarantID,RestuarantName,RestaurantAddress,MobileNo")] RestaurantDetail restaurantDetail)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.RestaurantDetails.Add(restaurantDetail);
                    //db.RestaurantDetails.Add(new RestaurantDetail()
                    //{

                    //    RestaurantAddress = restaurantDetail.RestaurantAddress,
                    //    MobileNo=restaurantDetail.MobileNo
                        
                    //});
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    
                    Console.WriteLine($"Data not inserted: '{ex.Message}'");
                    //restaurantDetail.ErrorMessgae = ex.Message;
                    return View(restaurantDetail);
                }
                
            }

            return View(restaurantDetail);
        }

 
        // GET: RestaurantDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RestaurantDetail restaurantDetail = db.RestaurantDetails.Find(id);
            if (restaurantDetail == null)
            {
                return HttpNotFound();
            }
            return View(restaurantDetail);
        }

        // POST: RestaurantDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RestuarantID,RestuarantName,RestaurantAddress,MobileNo")] RestaurantDetail restaurantDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(restaurantDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(restaurantDetail);
        }

        // GET: RestaurantDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RestaurantDetail restaurantDetail = db.RestaurantDetails.Find(id);
            if (restaurantDetail == null)
            {
                return HttpNotFound();
            }
            return View(restaurantDetail);
        }

        // POST: RestaurantDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RestaurantDetail restaurantDetail = db.RestaurantDetails.Find(id);
            db.RestaurantDetails.Remove(restaurantDetail);
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
