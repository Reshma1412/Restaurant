using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class ADOCuisinesController : ApiController
    {
        public IHttpActionResult GetCuisineDetails()
        {
            IList<InsertCuisineModel> cui = null;

            using (var ctx = new RestaurantEntities())
            {
                //cui = ctx.Cuisines.Include("Cuisine")
                //            .Select(s => new InsertCuisineModel()

                cui = ctx.Cuisines.Select(s => new InsertCuisineModel()
                {
                    cuisineid = s.CuisineID,
                    restaurantid = (int)s.RestuarantID,
                    cuisinename = s.CuisineName
                }).ToList<InsertCuisineModel>();
            }

            if (cui.Count == 0)
            {
                return NotFound();
            }

            return Ok(cui);
        }

        public IHttpActionResult GetCuisineById(int id)
        {
            InsertCuisineModel cui = null;

            using (var ctx = new RestaurantEntities())
            {
                cui = (InsertCuisineModel)ctx.Cuisines.Where(s => s.CuisineID == id)
                    .Select(s => new InsertCuisineModel()
                    {
                        cuisineid = s.CuisineID,
                        restaurantid = (int)s.RestuarantID,
                        cuisinename = s.CuisineName
                    }).FirstOrDefault<InsertCuisineModel>();
            }

            if (cui == null)
            {
                return NotFound();
            }

            return Ok(cui);
        }

        public IHttpActionResult PostNewCuisine(InsertCuisineModel inscui)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");

            using (var ctx = new RestaurantEntities())
            {
                ctx.Cuisines.Add(new Cuisine()
                {
                    CuisineID = inscui.cuisineid,
                    RestuarantID = inscui.restaurantid,
                    CuisineName = inscui.cuisinename
                });

                ctx.SaveChanges();
            }

            return Ok("Success");
        }

        public IHttpActionResult PutUpdateCuisine(InsertCuisineModel putcui)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            using (var ctx = new RestaurantEntities())
            {
                var existingStudent = ctx.Cuisines.Where(s => s.CuisineID == putcui.cuisineid)
                                                        .FirstOrDefault<Cuisine>();

                if (existingStudent != null)
                {
                    existingStudent.RestuarantID = putcui.restaurantid;
                    existingStudent.CuisineName = putcui.cuisinename;

                    ctx.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            }

            return Ok("Success");
        }

        public IHttpActionResult DeleteCuisineData(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid student id");

            using (var ctx = new RestaurantEntities())
            {
                var delcui = ctx.Cuisines
                    .Where(s => s.CuisineID == id)
                    .FirstOrDefault();

                ctx.Entry(delcui).State = System.Data.Entity.EntityState.Deleted;
                ctx.SaveChanges();
            }

            return Ok("Success");
        }
    }
}
