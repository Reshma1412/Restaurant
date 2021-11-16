using Newtonsoft.Json;
using Restaurant.BLL.Models;
using RestaurantMVCWithEF.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
//using System.Web.Http;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace RestaurantMVCWithEF.Controllers
{
    public class APINewCuisineController : Controller
    {
        // GET: APINewCuisine
        
        public ActionResult Index()
        {
            IEnumerable<APINewCuisine> cuisines = null;

            using (var client = new HttpClient())
            {

                //client.BaseAddress = new Uri("https://localhost:44337/api/");
                var path = ConfigurationManager.AppSettings["ApiBaseUri"];
                client.BaseAddress = new Uri(path);
                //HTTP GET
                var responseTask = client.GetAsync("APICuisine");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    //cuisines = readTask.Result;
                    cuisines = JsonConvert.DeserializeObject<List<APINewCuisine>>(readTask.Result);


                }
                else //web api sent error response 
                {
                    //log response status here..

                    cuisines = Enumerable.Empty<APINewCuisine>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(cuisines);
        }
    

        // GET: APINewCuisine/Details/5
        public ActionResult Details(int CID)
        {
            IEnumerable<APINewCuisine> cuisines = null;

            using (var client = new HttpClient())
            {

                //client.BaseAddress = new Uri("https://localhost:44337/api/");
                var path = ConfigurationManager.AppSettings["ApiBaseUri"];
                client.BaseAddress = new Uri(path);
                //HTTP GET
                var responseTask = client.GetAsync("APICuisine" + CID.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    //cuisines = readTask.Result;
                    cuisines = JsonConvert.DeserializeObject<List<APINewCuisine>>(readTask.Result);


                }
                else //web api sent error response 
                {
                    //log response status here..

                    cuisines = Enumerable.Empty<APINewCuisine>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(cuisines);
        }

        // GET: APINewCuisine/Create
        public ActionResult Create()
        {
            return View();
        }
        
        //POST: APINewCuisine/Create
       //[System.Web.Http.HttpPost]
       [HttpPost]
        public ActionResult Create(APINewCuisine cuisine)
        {
            try
            {
                // TODO: Add insert logic here
                using (var client = new HttpClient())
                {
                    //client.BaseAddress = new Uri("https://localhost:44337/api/");
                    var path = ConfigurationManager.AppSettings["ApiBaseUri"];
                    client.BaseAddress = new Uri(path);

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<APINewCuisine>("APICuisine", cuisine);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }

                ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

                return View(cuisine);
            
                
            }
            catch
            {
                return View();
            }
        }

        // GET: APINewCuisine/Edit/5
        public ActionResult Edit(int id)
        {
            //return View();

            APINewCuisine cuisine = null;

            using (var client = new HttpClient())
            {
                //client.BaseAddress = new Uri("https://localhost:44337/api/");
                var path = ConfigurationManager.AppSettings["ApiBaseUri"];
                client.BaseAddress = new Uri(path);
                //HTTP GET
                var responseTask = client.GetAsync("APICuisine?CId=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<APINewCuisine>();
                    readTask.Wait();

                    cuisine = readTask.Result;
                }
            }
            return View(cuisine);
            

        }
    

        // POST: APINewCuisine/Edit/5
        //[HttpPost]
        [HttpPost]
        //public ActionResult Edit(int id,APINewCuisine cuisine)
        //public ActionResult Edit(int id, APINewCuisine putcb)
        public ActionResult Edit(APINewCuisine cuisine)
        {
            try
            {
                // TODO: Add update logic here

                //return RedirectToAction("Index");
                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                using (var Objclient = new HttpClient())
                {
                    //Objclient.BaseAddress = new Uri("https://localhost:44337/api/");
                    var path = ConfigurationManager.AppSettings["ApiBaseUri"];
                    Objclient.BaseAddress = new Uri(path);
                    //HTTP POST
                    //var putTask = client.PutAsync("APICuisine/" + id.ToString(), new StringContent(new JavaScriptSerializer().Serialize(putcb), Encoding.UTF8, "application/json"));
                    //var putTask = client.PutAsync("APICuisine/", new StringContent(new JavaScriptSerializer().Serialize(cuisine), Encoding.UTF8, "application/json"));
                    //var putTask = client.PutAsJsonAsync<APINewCuisine>("putcb", cuisine);

                    Objclient.DefaultRequestHeaders.ConnectionClose = true;
                    
                    var putTask = Objclient.PutAsJsonAsync<APINewCuisine>("APICuisine", cuisine);

                    //var putTask = Objclient.PostAsJsonAsync<APINewCuisine>("APICuisine", cuisine);
                    putTask.Wait();

                    var result = putTask.Result;
                    if (result.IsSuccessStatusCode)
                    {

                        return RedirectToAction("Index");
                    }
                }
                return View(cuisine);

            }
            catch //(Exception ex)
            {
                return View();
            }
    
            
        }

        // GET: APINewCuisine/Delete/5
        public ActionResult Delete(int id)
        //public ActionResult Delete(APINewCuisine cuisine)
        {
            try
            {
                //return View();
                using (var client = new HttpClient())
                {
                    //client.BaseAddress = new Uri("https://localhost:44337/api/");
                    var path = ConfigurationManager.AppSettings["ApiBaseUri"];
                    client.BaseAddress = new Uri(path);

                    //HTTP DELETE
                    var deleteTask = client.DeleteAsync("APICuisine/" + id.ToString());
                    //var deleteTask = client.DeleteAsync("APICuisine", cuisine);

                    deleteTask.Wait();



                    var result = deleteTask.Result;
                    if (result.IsSuccessStatusCode)
                    {

                        return RedirectToAction("Index");
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

    }
}
