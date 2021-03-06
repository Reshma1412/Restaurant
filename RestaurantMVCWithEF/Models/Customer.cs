//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RestaurantMVCWithEF.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public partial class Customer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customer()
        {
            this.Bills = new HashSet<Bill>();
        }
    
        public int CustomerID { get; set; }

        [Required]
        public int RestuarantID { get; set; }

        [Required]
        [MinLength(10, ErrorMessage = "Minimum 10 characters are required for name field")]
        [RegularExpression(@"^[A-Za-z ]+$", ErrorMessage ="Name should contain only letters and space")]
        ///^[a-zA-Z\s]*$/
        //    /^[a-zA-Z ]*$/
        public string CustomerName { get; set; }
        
        [Required]        
        [MinLength(10, ErrorMessage = "Minimum 10 digits are required for Mobile Number")]
        public string MobileNo { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bill> Bills { get; set; }
        public virtual RestaurantDetail RestaurantDetail { get; set; }
    }
}
