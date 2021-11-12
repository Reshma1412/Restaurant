using RestaurantMVCWithEF.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RestaurantMVCWithEF.Controllers
{
    public class DisplayCustomerDetailsController : Controller
    {
        // GET: DisplayCustomerDetails
        public ActionResult Index()
        {
            RestaurantEntities1 re = new RestaurantEntities1();


            var context = new RestaurantEntities1();

            //var query = context.Database.SqlQuery<>("EXEC [dbo].[USPYearTotalAmount] @RestuarantID", new SqlParameter("RestuarantID", 19)).ToList();
            //return View(query);

            //context.Database.SqlQuery<myEntityType>("mySpName @param1, @param2, @param3",new SqlParameter("param1", param1),new SqlParameter("param2", param2),new SqlParameter("param3", param3));

            var query = context.Database.SqlQuery<USPDisplayCustomerDetails>("EXEC [dbo].[USPDisplayCustomerDetails] @FilterBy,@OrderBy", new SqlParameter("FilterBy", DBNull.Value), new SqlParameter("@OrderBy", DBNull.Value)).ToList();
            return View(query.ToArray());
        }

        [HttpPost]
        public ActionResult Index(string FilterBy,string OrderBy)
        {
            var context = new RestaurantEntities1();
            if (string.IsNullOrEmpty(FilterBy) && string.IsNullOrEmpty(OrderBy))
            {
                var query = context.Database.SqlQuery<USPDisplayCustomerDetails>("EXEC [dbo].[USPDisplayCustomerDetails] @FilterBy,@OrderBy", new SqlParameter("FilterBy", DBNull.Value), new SqlParameter("@OrderBy", DBNull.Value)).ToList();
                return View(query.ToArray());

            }
            else if (string.IsNullOrEmpty(FilterBy) || string.IsNullOrEmpty(OrderBy))
            {
                if (!string.IsNullOrEmpty(FilterBy) || string.IsNullOrEmpty(OrderBy))
                {
                    var query = context.Database.SqlQuery<USPDisplayCustomerDetails>("EXEC [dbo].[USPDisplayCustomerDetails] @FilterBy,@OrderBy", new SqlParameter("FilterBy", FilterBy), new SqlParameter("@OrderBy", DBNull.Value)).ToList();
                    return View(query.ToArray());

                }
                else if (string.IsNullOrEmpty(FilterBy) || !string.IsNullOrEmpty(OrderBy))
                {
                    var query = context.Database.SqlQuery<USPDisplayCustomerDetails>("EXEC [dbo].[USPDisplayCustomerDetails] @FilterBy,@OrderBy", new SqlParameter("FilterBy", DBNull.Value), new SqlParameter("@OrderBy", OrderBy)).ToList();
                    return View(query.ToArray());

                }
            }
            else
            {
                var query = context.Database.SqlQuery<USPDisplayCustomerDetails>("EXEC [dbo].[USPDisplayCustomerDetails] @FilterBy,@OrderBy", new SqlParameter("FilterBy", FilterBy), new SqlParameter("@OrderBy", OrderBy)).ToList();
                return View(query.ToArray());

            }

            return View();

        }

    }
}