using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SamuEcommerce_Web_App.Models.Inventory
{
    public class InventoryCreateModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public IEnumerable<SelectListItem> Product { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public short Status { get; set; }
    }
}