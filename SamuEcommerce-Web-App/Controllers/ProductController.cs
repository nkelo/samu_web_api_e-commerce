using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using SamuEcommerce_Web_App.Models.Products;
using System.Web.Script.Serialization;
using Ecommerce_API.Models.Orders;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using SamuEcommerce_Web_App.Models.Orders;
using System.Net.Mail;
using System.Net;

namespace SamuEcommerce_Web_App.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Index()
        {
            List<ProductListModel> products = new List<ProductListModel>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:60609/api/Product");

                HttpResponseMessage response = client.GetAsync(client.BaseAddress + string.Format("/Index")).Result;

                if (response.IsSuccessStatusCode)
                {
                    products = (new JavaScriptSerializer()).Deserialize<List<ProductListModel>>(response.Content.ReadAsStringAsync().Result);
                }

            }
                return View(products);
        }


        // GET: Product
        [HttpGet]
        public ActionResult ListProducts()
        {
            List<ProductViewModel> products = new List<ProductViewModel>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:60609/api/Product");

                HttpResponseMessage response = client.GetAsync(client.BaseAddress + string.Format("/productList")).Result;

                if (response.IsSuccessStatusCode)
                {
                    products = (new JavaScriptSerializer()).Deserialize<List<ProductViewModel>>(response.Content.ReadAsStringAsync().Result);
                }

            }
            return View(products);
        }

        [Authorize]
        //[HttpPost]
        public JsonResult PlaceOrder(string model)
        {
            
            try
            {
                // TODO: Add insert logic here

                List<OrderTempModel> models = (new JavaScriptSerializer()).Deserialize<List<OrderTempModel>>(model);
                var bodyJoin = "";
                double total = 0;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:60609/api/Product");

                    Models.Orders.OrderModel order = new Models.Orders.OrderModel
                    {
                        UserId = User.Identity.GetUserId(),
                        OrderDateTime = DateTime.Now,
                        ModifiedDateTime = DateTime.Now,
                        Status = 1
                    };
                    
                    //HTTP POST
                    var postTask = client.PostAsJsonAsync(client.BaseAddress + string.Format("/PlaceOrder"), order);
                    postTask.Wait();

                   
                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var id = result.Content.ReadAsStringAsync().Result;

                        foreach (var item in models) {

                            OrderItemModel orderItem = new OrderItemModel
                            {
                                OrderId = Int32.Parse(id),
                                ProductId = item.ProductId,
                                Quantity = item.Quantity,
                                CreatedDateTime = DateTime.Now,
                                ModifiedDateTime = DateTime.Now,
                                Status = 1

                            };

                            postTask = client.PostAsJsonAsync(client.BaseAddress + string.Format("/AddOrderItem"), orderItem);
                            postTask.Wait();

                            result = postTask.Result;
                            if (!result.IsSuccessStatusCode) {

                                return Json(new { Status = 0, Message = "Unsuccessful" }, JsonRequestBehavior.AllowGet);
                            }

                            total = total + (item.Price * item.Quantity);
                            bodyJoin = bodyJoin + "<tr>" +
                             //"<td> Product Name</td>" +
                             "<td>" + item.Name + "</td >" +
                             "<td>R" + item.Price + "</td >" +
                             "<td>" + item.Quantity + "</td >" +
                             "<td></td >" +
                             "</tr>";
    
                        
                              
                                              
                        }

                        //bodyJoin = bodyJoin + "</ table >";
                        //return RedirectToAction("Index");

                        // Send email to client 
                        var senderEmail = new MailAddress("simukelontshaba@gmail.com", "Samu");
                        var receiverEmail = new MailAddress(User.Identity.GetUserName(), "Customer");
                        var password = "Nkelo@12831994";
                        var subject = "Order Placement";
                        var body = "<h3>Dear Customer</h3>" +
                            "<h3>Thank you for placing an order with us.</h3>" +
                            "<h2> Please see your order information below </h2> " +
                            " " +
                            "<h2> Order # "+ id + "</h2><br>" +
                            "<table border =\"1\" align=\"left\" cellpadding=\0\" cellspacing=\"0\">" +
                            "<thead>" +
                            "<tr>" +
                            "<th>Product Name</th>" +
                            "<th>Unit Price</th>" +
                            "<th>Quantity</th>" +
                             "<th>Total</th>" +
                            "</tr>" +
                            "</thead>" +                     
                            bodyJoin + "" +
                            "<tr>" +
                            "<td></td>"+
                            "<td></td>" +
                            "<td></td>" +
                            "<td>R"+total+"</td>" +
                            "</tr>" +
                            "</table>";
                        var smtp = new SmtpClient
                        {
                            Host = "smtp.gmail.com",
                            Port = 587,
                            EnableSsl = true,
                            DeliveryMethod = SmtpDeliveryMethod.Network,
                            UseDefaultCredentials = false,
                            Credentials = new NetworkCredential(senderEmail.Address, password)
                        };
                        using (var mess = new MailMessage(senderEmail, receiverEmail)
                        {
                            Subject = subject,
                            Body = body
                        })
                        {
                            mess.IsBodyHtml = true;
                            smtp.Send(mess);
                        }



                    }
                }

                ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

                //return View(student);

                return Json(new { Status = 1, Message = "Successful" }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(new { Status = 0, Message = "Unsuccessful" }, JsonRequestBehavior.AllowGet);
            }

            //return Json(new { Status = 1, Message = "Successful!"}, JsonRequestBehavior.AllowGet);
        }

        // GET: Product/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Product/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection, ProductCreateModel model)
        {
            try
            {
                // TODO: Add insert logic here

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:60609/api/Product");

                    model.CreatedDateTime = DateTime.Now;
                    model.ModifiedDateTime = DateTime.Now;
                    model.Status = 1;
                    //HTTP POST
                    var postTask = client.PostAsJsonAsync(client.BaseAddress + string.Format("/Create"), model);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }

                ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

                //return View(student);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            ProductEditModel product = new ProductEditModel();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:60609/api/Product");

                HttpResponseMessage response = client.GetAsync(client.BaseAddress + string.Format("/GetById/"+id)).Result;

                if (response.IsSuccessStatusCode)
                {
                    product = (new JavaScriptSerializer()).Deserialize<ProductEditModel>(response.Content.ReadAsStringAsync().Result);
                }

            }

            return View(product);
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection, ProductEditModel model)
        {
            try
            {
                // TODO: Add update logic here

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:60609/api/Product");

                    //model.CreatedDateTime = DateTime.Now;
                    model.ModifiedDateTime = DateTime.Now;
                    //model.Status = 1;
                    //HTTP POST
                    var postTask = client.PostAsJsonAsync(client.BaseAddress + string.Format("/Edit/"+id), model);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }

                ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

                //return View(student);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Product/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
