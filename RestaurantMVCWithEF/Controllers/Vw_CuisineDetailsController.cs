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
    public class Vw_CuisineDetailsController : Controller
    {
        public ActionResult Index()
        {
            var context = new RestaurantEntities1();
            var query = context.Database.SqlQuery<Vw_CuisineDetails>("Select * from Vw_CuisineDetails").ToList();
            //var query = context.Database.SqlQuery("SELECT * FROM Vw_CuisineDetails");
            return View(query);
        }

    }
}
