//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataContext
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductInventory
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public System.DateTime CreatedDateTime { get; set; }
        public System.DateTime ModifiedDateTime { get; set; }
        public short Status { get; set; }
    }
}