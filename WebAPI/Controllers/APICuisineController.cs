using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Restaurant.BLL.Models;

namespace WebAPI.Controllers
{
    public class APICuisineController : ApiController
    {
        CuisineBLL cb = new CuisineBLL();
        public IEnumerable<CuisineBLL> Get()
        {
            IList<CuisineBLL> GetCuisineList = cb.SelectCuisineData();
            return GetCuisineList;
            
        }

        public IHttpActionResult Get(int CId)
        {
            return Ok(cb.SelectCuisineData ().FirstOrDefault(x => x.CuisineID == CId));
        }

        
        
        public IHttpActionResult PostAddNewCuisine([FromBody] CuisineBLL postcb)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Invalid data.");

                if(cb.InsertCuisine(postcb))
                    return Ok("Success");
                else
                    return Ok("Something went wrong, try later");


            }

            catch
            {
                return Ok("Something went wrong, try later");
            }
        }

        [HttpPut]
        public IHttpActionResult PutUpdateCuisineData([FromBody] CuisineBLL putcb)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Invalid data.");

                if (cb.UpdateCuisine(putcb))
                    return Ok("Success");
                else
                    return Ok("Something went wrong, try later");
            }

            catch
            {
                return Ok("Something went wrong, try later");
            }
        }

        //public IHttpActionResult DeleteCuisineData([FromBody] CuisineBLL delcb)
        //public IHttpActionResult DeleteCuisineData(CuisineBLL delcb)
        //public IHttpActionResult DeleteCuisineData([FromBody] int id)



        public IHttpActionResult DeleteCuisineData(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Invalid data.");

                var delcb = new CuisineBLL();
                delcb.CuisineID = id;
                delcb = cb.GetAllCuisines().FirstOrDefault(Cui => Cui.CuisineID == id);
                if (cb.DeleteCuisine(delcb))
                    return Ok("Success");
                else
                    return Ok("Something went wrong, try later");

            }
            catch
            {
                return Ok("Something went wrong, try later");
            }
        }


        //public IHttpActionResult DeleteCuisineData([FromBody] CuisineBLL delcb)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //            return BadRequest("Invalid data.");


        //        //var delcb = new CuisineBLL();
        //        ////delcb.CuisineID = id;
        //        //delcb = cb.GetAllCuisines().Find(Cui => Cui.CuisineID == id);
        //        if (cb.DeleteCuisine(delcb))
        //            return Ok("Success");
        //        else
        //           return Ok("Something went wrong, try later");

        //    }
        //    catch
        //    {
        //        return Ok("Something went wrong, try later");
        //    }



        //}

    }
}
