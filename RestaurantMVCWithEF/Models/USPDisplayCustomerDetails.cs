using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantMVCWithEF.Models
{
    public class USPDisplayCustomerDetails
    {
        public string CustomerName { get; set; }
        public string MobileNo { get; set; }
        public int OrderID { get; set; }
        public System.DateTime OrderDate { get; set; }
        public double OrderAmount { get; set; }
        public string CuisineName { get; set; }
        public string ItemName { get; set; }
        public Nullable<double> ItemPrice { get; set; }
        public string DiningLocation { get; set; }
    }
}
