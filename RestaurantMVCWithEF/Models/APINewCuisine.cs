using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantMVCWithEF.Models
{
    public class APINewCuisine
    {
        public int CuisineID { get; set; }

        public Nullable<int> RestaurantID { get; set; }

        public string CuisineName { get; set; }
    }
}