//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Restaurant.Models
{
    using System;
    
    public partial class BillsTableFunction_Result
    {
        public int OrderID { get; set; }
        public System.DateTime OrderDate { get; set; }
        public int RestuarantID { get; set; }
        public int MenuItemID { get; set; }
        public int ItemQuantity { get; set; }
        public double OrderAmount { get; set; }
        public int DiningTableID { get; set; }
    }
}
