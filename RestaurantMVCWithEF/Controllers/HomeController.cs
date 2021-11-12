using RestaurantMVCWithEF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace RestaurantMVCWithEF.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //RestaurantEntities entities = new RestaurantEntities();
            //return View(entities.TableStatus(null));
            return View();
        }
        //public ActionResult Index()
        //{
        //    RestaurantEntities entities = new RestaurantEntities();
        //    return View(entities.TableStatus());
        //    //return View();
        //}

        //[HttpPost]
        //public ActionResult Index(int RestuarantID)
        //{

        //    //if (Request.Form["text"] == null)  
        //    //{  
        //    //    TempData["SelectOption"] = -1;  
        //    //}             
        //    RestaurantEntities entities = new RestaurantEntities();
        //    return View(entities.TableStatus(RestuarantID));
        //}


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Order()
        {
            return View();
        }

        public ActionResult Bills()
        {
            return View();
        }
    }
}