using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Restaurant.BLL.Models;
using System.Web.Mvc;


namespace WebApiRestaurant.Controllers
{
    public class RestaurantAPIController : ApiController
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

        

        // GET: api/RestaurantAPI/5
        //public RestaurantDetailsBLL  Get(int Rid)
        //{
        //    return rdb.SelectData().firstordefault();
        //}

    // POST: api/RestaurantAPI
    public void Post([FromBody]string value)
        {
        }

        // PUT: api/RestaurantAPI/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/RestaurantAPI/5
        public void Delete(int id)
        {
        }
    }
}
