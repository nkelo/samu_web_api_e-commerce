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
    
    public partial class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public System.DateTime OrderDateTime { get; set; }
        public Nullable<System.DateTime> ModifiedDateTime { get; set; }
        public short Status { get; set; }
    }
}