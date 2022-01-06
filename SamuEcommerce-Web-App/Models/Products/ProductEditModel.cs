using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SamuEcommerce_Web_App.Models.Products
{
    public class ProductEditModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double UnitPrice { get; set; }
        public string Image { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
        public int Status { get; set; }
    }
}