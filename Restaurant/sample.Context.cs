//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Restaurant
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class RestaurantEntities1 : DbContext
    {
        public RestaurantEntities1()
            : base("name=RestaurantEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
    
        public virtual int USPDisplayCustomerDetails(string filterBy, string orderBy)
        {
            var filterByParameter = filterBy != null ?
                new ObjectParameter("FilterBy", filterBy) :
                new ObjectParameter("FilterBy", typeof(string));
    
            var orderByParameter = orderBy != null ?
                new ObjectParameter("OrderBy", orderBy) :
                new ObjectParameter("OrderBy", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("USPDisplayCustomerDetails", filterByParameter, orderByParameter);
        }
    }
}
