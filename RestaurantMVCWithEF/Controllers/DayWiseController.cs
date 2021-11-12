using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestaurantMVCWithEF.Models;

namespace RestaurantMVCWithEF.Controllers
{
    public class DayWiseController : Controller
    {
        // GET: ResYearWise
        public ActionResult Index()
        {
            var context = new RestaurantEntities1();
            var query = context.Database.SqlQuery<USPDayWiseAmount_Result>("EXEC [dbo].[USPDayWiseAmount] @RestuarantID", new SqlParameter("RestuarantID", DBNull.Value)).ToList();
            //var query = context.Database.SqlQuery<USPYearTotalAmount_Result>("EXEC [dbo].[USPYearTotalAmount] @RestuarantID", new SqlParameter("RestuarantID", 19)).ToList();
            //return View(query.toList());
            //return View();
            return View(query);
        }

        [HttpPost]
        public ActionResult Index(int? RestaurantID)
        {
            if (RestaurantID == null)
            {
                var context = new RestaurantEntities1();
                var query = context.Database.SqlQuery<USPDayWiseAmount_Result>("EXEC [dbo].[USPDayWiseAmount] @RestuarantID", new SqlParameter("RestuarantID", DBNull.Value)).ToList();
                return View(query.ToArray());
                //return View();

            }
            else
            {
                var context = new RestaurantEntities1();
                var query = context.Database.SqlQuery<USPDayWiseAmount_Result>("EXEC [dbo].[USPDayWiseAmount] @RestuarantID", new SqlParameter("RestuarantID", RestaurantID)).ToList();
                return View(query.ToArray());
                //return View();
            }

        }
    }
}