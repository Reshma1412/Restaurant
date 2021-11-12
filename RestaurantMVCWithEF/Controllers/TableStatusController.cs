using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using RestaurantMVCWithEF.Models;
using System.Data.SqlClient;

namespace RestaurantMVCWithEF.Controllers
{
    public class TableStatusController : Controller
    {
        // GET: TableStatus
        public ActionResult Index()
        {

            var context = new RestaurantEntities1();
            var result = context.UFDTableStatus(null);
            //var result = context.Database.SqlQuery<UFDTableStatus_Result>("EXEC [dbo].[UFDTableStatus] @RestuarantID", new SqlParameter("RestuarantID", DBNull.Value)).ToList();
            return View(result.ToArray());
            //return View();
        }
        
        [HttpPost]
        public ActionResult Index(int? RestaurantID)
        {
            if (RestaurantID == null)
            {
                var context = new RestaurantEntities1();
                var result = context.UFDTableStatus(null);
                //var result = context.Database.SqlQuery<UFDTableStatus_Result>("EXEC [dbo].[UFDTableStatus] @RestuarantID", new SqlParameter("RestuarantID", null)).ToList();
                return View(result.ToArray());
                //return View();

            }
            else
            {
                var context = new RestaurantEntities1();
                //var result = context.UFDTableStatus(20);
                var result = context.Database.SqlQuery<UFDTableStatus_Result>("EXEC [dbo].[UFDTableStatus] @RestuarantID", new SqlParameter("RestuarantID", RestaurantID)).ToList();
                return View(result.ToArray());
                //return View();
            }

        }


    }
}