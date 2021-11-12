using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Restaurant.BLL.Models;

namespace RestaurantMVCWithADO.Controllers
{
    public class CuisineController : Controller
    {
        // GET: Employee/GetAllEmpDetails    
        public ActionResult GetAllCuisineDetails()
        {

            CuisineBLL RDB = new CuisineBLL();
            ModelState.Clear();
            return View(RDB.GetAllCuisines());
        }
        // GET: Employee/AddEmployee    
        public ActionResult AddCuisine()
        {
            return View();
        }

        // POST: Employee/AddEmployee    
        [HttpPost]
        public ActionResult AddCuisine(CuisineBLL cui)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CuisineBLL RDB = new CuisineBLL();
                    if (RDB.InsertCuisine(cui))
                    {
                        ViewBag.Message = "Cuisine details added successfully";
                    }

                    //RDB.InsertCuisine(cui)
                }

                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/EditEmpDetails/5    
        public ActionResult EditCuisineDetails(int id)
        {
            CuisineBLL RDB = new CuisineBLL();



            return View(RDB.GetAllCuisines().Find(Cui => Cui.CuisineID == id));

        }

        // POST: Employee/EditEmpDetails/5    
        [HttpPost]

        public ActionResult EditCuisineDetails(CuisineBLL cui)
        {
            try
            {
                CuisineBLL RDB = new CuisineBLL();

                RDB.UpdateCuisine(cui);
                return RedirectToAction("GetAllCuisineDetails");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/DeleteEmp/5    
        public ActionResult DeleteCui(int id)
        {
            try
            {
                CuisineBLL RDB = new CuisineBLL();

                if (RDB.DeleteCuisine(RDB.GetAllCuisines().Find(Cui => Cui.CuisineID == id)))
                {
                    ViewBag.AlertMsg = "Cuisine details deleted successfully";

                }
                return RedirectToAction("GetAllCuisineDetails");

            }
            catch
            {
                return View();
            }
        }

    }
}
