using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace RestaurantMVCWithEF.ViewModels
{
    public class RestaurantDetailViewModel
    {

        public int RestuarantID { get; set; }
        [Required]
        public string RestuarantName { get; set; }
        [RegularExpression(@"^[0-9].*", ErrorMessage = "Only two decimal places are allowed")]
        public string RestaurantAddress { get; set; }
        
        [Required]
        [Remote("ChkNoValidation", "RestaurantDetails", ErrorMessage = "Mobile number is already exists")]
        public string MobileNo { get; set; }

        public string ErrorMessgae { get; set; }
    }
}