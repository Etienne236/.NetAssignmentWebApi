using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ProductController : Controller
    {
        private readonly HttpClient _httpClient;
        public ProductController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        // GET: ProductController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(IFormCollection collection)
        {
            try
            {
                // Map the form collection data to the Product model
                Product product = new Product
                {
                    Name = collection["Name"],
                    Quantity = int.Parse(collection["Quantity"])
                };

                // Make a POST request to the Web API's CreateProduct method
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync("/api/Product", product);

                if (response.IsSuccessStatusCode)
                {
                    // Product created successfully
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Handle the error response
                    // Redirect to an error view or display an error message
                    return View();
                }
            }
            catch
            {
                // Exception occurred
                // Redirect to an error view or display an error message
                return View();
            }
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
