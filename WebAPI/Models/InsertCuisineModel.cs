using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class InsertCuisineModel
    {
        public int cuisineid{get;set; }

        public int restaurantid { get; set; }

        public string cuisinename { get; set; }
    }
}