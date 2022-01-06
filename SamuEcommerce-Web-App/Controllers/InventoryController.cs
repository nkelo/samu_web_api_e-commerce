using SamuEcommerce_Web_App.Models.Inventory;
using SamuEcommerce_Web_App.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SamuEcommerce_Web_App.Controllers
{
    public class InventoryController : Controller
    {
        // GET: Inventory
        public ActionResult Index()
        {
            List<InventoryListModel> inventories = new List<InventoryListModel>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:60609/api/Product");

                HttpResponseMessage response = client.GetAsync(client.BaseAddress + string.Format("/Inventory")).Result;

                if (response.IsSuccessStatusCode)
                {
                    inventories = (new JavaScriptSerializer()).Deserialize<List<InventoryListModel>>(response.Content.ReadAsStringAsync().Result);
                }

            }
            return View(inventories);
        }

        // GET: Inventory/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Inventory/Create
        public ActionResult Create()
        {
            InventoryCreateModel model = new InventoryCreateModel();
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

            var productList = new List<SelectListItem>();
            foreach (var element in products)
            {
                productList.Add(new SelectListItem
                {
                    Value = element.Id.ToString(),
                    Text = element.Name
                });
            }

            model.Product = productList;


            return View(model);

            //return View();
        }

        // POST: Inventory/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection, InventoryListModel model)
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
                    var postTask = client.PostAsJsonAsync(client.BaseAddress + string.Format("/CreateInventory"), model);
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

        // GET: Inventory/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Inventory/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Inventory/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Inventory/Delete/5
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
