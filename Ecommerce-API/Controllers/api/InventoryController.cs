using DataContext;
using Ecommerce_API.Models.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Ecommerce_API.Controllers.api
{
    public class InventoryController : ApiController
    {

        NewEcommerceDBEntities entities = new NewEcommerceDBEntities();

        //[AcceptVerbs("GET")]
        //[System.Web.Http.Route("api/Inventory/Inventory")]
        //public IHttpActionResult Inventory()
        //{
        //    List<InventoryListModel> inventories = new List<InventoryListModel>();
        //    var myEntities = entities.ProductInventories;

        //    foreach (ProductInventory model in myEntities)
        //    {
        //        inventories.Add(new InventoryListModel
        //        {
        //            Id = model.Id,
        //            ProductId = model.ProductId,
        //            Quantity = model.Quantity,
        //            CreatedDateTime = model.CreatedDateTime,
        //            ModifiedDateTime = model.ModifiedDateTime,
        //            Status = model.Status
        //        });
        //    }


        //    return Json(inventories);
        //}

        // GET: api/Inventory
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Inventory/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Inventory
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Inventory/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Inventory/5
        public void Delete(int id)
        {
        }
    }
}
