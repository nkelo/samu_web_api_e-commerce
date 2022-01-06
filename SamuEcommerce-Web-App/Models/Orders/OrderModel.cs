using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SamuEcommerce_Web_App.Models.Orders
{
    public class OrderModel
    {

        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime OrderDateTime { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
        public int Status { get; set; }

    }
}