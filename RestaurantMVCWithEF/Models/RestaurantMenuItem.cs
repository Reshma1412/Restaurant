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

    public partial class RestaurantMenuItem
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RestaurantMenuItem()
        {
            this.OrderInfoes = new HashSet<OrderInfo>();
        }
    
        public int MenuItemID { get; set; }

        [Required]
        public int CuisineID { get; set; }

        [Required]
        public string ItemName { get; set; }

        [Required]
        [RegularExpression(@"^[1-9] [0-9]*$",ErrorMessage ="Item Price should be greater than 0")]
        public Nullable<double> ItemPrice { get; set; }
    
        public virtual Cuisine Cuisine { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderInfo> OrderInfoes { get; set; }
    }
}