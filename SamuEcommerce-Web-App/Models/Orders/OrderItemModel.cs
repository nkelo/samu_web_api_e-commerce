using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SamuEcommerce_Web_App.Models.Orders
{
    public class OrderItemModel
    {

        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
        public int Status { get; set; }

    }
}