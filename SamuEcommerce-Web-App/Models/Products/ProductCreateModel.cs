using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SamuEcommerce_Web_App.Models.Products
{
    public class ProductCreateModel
    {
        //public int Id { get; set; }
        public string Name { get; set; }
        public float UnitPrice { get; set; }
        public string Image { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
        public short Status { get; set; }
    }
}