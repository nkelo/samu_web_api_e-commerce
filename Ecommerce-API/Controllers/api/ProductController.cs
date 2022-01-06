using DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ecommerce_API.Models.Products;
using System.Web.Http;
using AcceptVerbsAttribute = System.Web.Http.AcceptVerbsAttribute;
using System.Net.Http;
using System.Net;
using Ecommerce_API.Models.Inventory;

namespace Ecommerce_API.Controllers
{
    public class ProductController : ApiController
    {

        NewEcommerceDBEntities entities = new NewEcommerceDBEntities();

        [AcceptVerbs("GET")]
        [System.Web.Http.Route("api/Product/Index")]
        public IHttpActionResult Index()
        {
            List<ProductModel> myProducts = new List<ProductModel>();
            var myEntities = entities.Products;

            foreach (Product model in myEntities) {
                myProducts.Add(new ProductModel
                {
                    CreatedDateTime = model.CreatedDateTime,
                    Id = model.Id,
                    Image = model.Image,
                    ModifiedDateTime = model.ModifiedDateTime,
                    Name = model.Name,
                    Status = model.Status,
                    UnitPrice = model.UnitPrice
                });
            }


            return Json(myProducts);
        }

        [AcceptVerbs("GET")]
        [System.Web.Http.Route("api/Product/GetById/{id}")]
        public IHttpActionResult GetById(int id)
        {

            var myEntitie = entities.Products.Find(id); //.Where(i => i.Id == id).FirstOrDefault();

            ProductModel myProduct = new ProductModel {
                Id = myEntitie.Id,
               CreatedDateTime = myEntitie.CreatedDateTime,
              Image = myEntitie.Image,
              ModifiedDateTime = myEntitie.ModifiedDateTime,
              Name = myEntitie.Name,
              Status = myEntitie.Status,
              UnitPrice = myEntitie.UnitPrice
            };


            return Json(myProduct);
        }

        [AcceptVerbs("GET")]
        [System.Web.Http.Route("api/Product/productList")]
        public IHttpActionResult productList()
        {
                        
            var productInventory = (from p in entities.Products join i in entities.ProductInventories on p.Id equals i.ProductId
                                    select new { p.Id, p.Name, p.Status, p.UnitPrice, p.Image, p.ModifiedDateTime, p.CreatedDateTime, i.Quantity}).ToList();            

            return Json(productInventory);
        }


        [AcceptVerbs("GET")]
        [System.Web.Http.Route("api/Product/Inventory")]
        public IHttpActionResult Inventory()
        {
            List<InventoryListModel> inventories = new List<InventoryListModel>();
            var myEntities = entities.ProductInventories;

            foreach (ProductInventory model in myEntities)
            {
                inventories.Add(new InventoryListModel
                {
                    Id = model.Id,
                    ProductId = model.ProductId,
                    Quantity = model.Quantity,
                    CreatedDateTime = model.CreatedDateTime,
                    ModifiedDateTime = model.ModifiedDateTime,
                    Status = model.Status
                });
            }


            return Json(inventories);
        }

        //[AcceptVerbs("GET")]
        //[System.Web.Mvc.Route("api/Product/getInventories")]
        //public IHttpActionResult getInventories()
        //{
        //    List<InventoryListModel> inventories = new List<InventoryListModel>();

        //    return Json(inventories);
        //}

        [AcceptVerbs("POST")]
        [System.Web.Http.Route("api/Product/Create")]
        public HttpResponseMessage Create([FromBody]Product model)
        {

            try
            {
                entities.Products.Add(model);
                entities.SaveChanges();
                //string Result = 
                return Request.CreateResponse(HttpStatusCode.Created, model);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [AcceptVerbs("POST")]
        [System.Web.Http.Route("api/Product/Edit/{id}")]
        public HttpResponseMessage Edit([FromBody]Product model, int id)
        {

            try
            {               

                Product product = entities.Products.FirstOrDefault(i => i.Id == model.Id);
                product.Image = model.Image;
                product.Name = model.Name;
                product.Status = model.Status;
                product.UnitPrice = model.UnitPrice;
                product.ModifiedDateTime = model.ModifiedDateTime;


                entities.SaveChanges();
                //string Result = 
                return Request.CreateResponse(HttpStatusCode.OK, product);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        [AcceptVerbs("POST")]
        [System.Web.Http.Route("api/Product/CreateInventory")]
        public HttpResponseMessage CreateInventory([FromBody]ProductInventory model)
        {

            try
            {
                entities.ProductInventories.Add(model);
                entities.SaveChanges();
                //string Result = 
                return Request.CreateResponse(HttpStatusCode.Created, model);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [AcceptVerbs("POST")]
        [System.Web.Http.Route("api/Product/AddOrderItem")]
        public HttpResponseMessage AddOrderItem([FromBody]OrderItem model)
        {

            try
            {
                entities.OrderItems.Add(model);
                entities.SaveChanges();
                //string Result = 
                return Request.CreateResponse(HttpStatusCode.Created, model);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [AcceptVerbs("POST")]
        [System.Web.Http.Route("api/Product/PlaceOrder")]
        public int PlaceOrder([FromBody]Order model)
        {

            try
            {
                entities.Orders.Add(model);
                entities.SaveChanges();
                //string Result = 
                return model.Id;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }


    }
}
