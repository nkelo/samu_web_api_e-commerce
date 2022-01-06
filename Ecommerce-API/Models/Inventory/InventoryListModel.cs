using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce_API.Models.Inventory
{
    public class InventoryListModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public short Status { get; set; }
    }
}