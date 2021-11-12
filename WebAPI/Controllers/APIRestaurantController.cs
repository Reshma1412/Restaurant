using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Restaurant.BLL.Models;


namespace WebAPI.Controllers
{
    public class APIRestaurantController : ApiController
    {
        
        RestaurantDetailsBLL rdb = new RestaurantDetailsBLL();
        // GET: api/RestaurantAPI
        public IEnumerable<RestaurantDetailsBLL> Get()
        {
            IList<RestaurantDetailsBLL> GetRestaurantList = rdb.SelectRestaurantData();
           
            
            return GetRestaurantList;
            //return GetRestaurantList;
            //return new string[] { "value1", "value2" };

        }

        public RestaurantDetailsBLL Get(int RId)
        {
            return rdb.SelectRestaurantData().FirstOrDefault(x => x.ID == RId);
        }

        //[HttpPost]
        public IHttpActionResult PostAddNewRestaurant([FromBody] RestaurantDetailsBLL postrdb)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Invalid data.");

                rdb.InsertRestaurant(postrdb);
                return Ok("Success");

            }

            catch
            {
                return Ok("Something went wrong, try later");
            }
        }

        
        public IHttpActionResult PutUpdateRestaurantData([FromBody] RestaurantDetailsBLL putrdb)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Invalid data.");

                rdb.UpdateRestaurant(putrdb);
                return Ok("Success");

            }

            catch
            {
                return Ok("Something went wrong, try later");
            }
        }

        public IHttpActionResult DeleteRestaurantData([FromBody] RestaurantDetailsBLL delrdb)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Invalid data.");

                rdb.DeleteRestaurant(delrdb);
                return Ok("Success");

            }

            catch
            {
                return Ok("Something went wrong, try later");
            }
        }
    }
}
