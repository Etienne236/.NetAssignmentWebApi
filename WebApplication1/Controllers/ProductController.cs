using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using WebApplication1.Models;
using Newtonsoft.Json;
using System.Text;
using WebApi1.Data;

namespace WebApplication1.Controllers
{
    public class ProductController : Controller
    {
        private readonly HttpClient _httpClient;

        public ProductController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:44317/"); // Replace with your Web API base URL
        }

        // GET: ProductController
        public async Task<ActionResult> Index()
        {
            List<Product> products = new List<Product>();

            // Make a GET request to the Web API's ExecuteProcedure method
            HttpResponseMessage response = await _httpClient.GetAsync("/api/product");

            if (response.IsSuccessStatusCode)
            {
                // Read the response content
                string responseContent = await response.Content.ReadAsStringAsync();

                // Deserialize the response content to a list of products
                products = JsonConvert.DeserializeObject<List<Product>>(responseContent);
            }
            else
            {
                // Handle the error response
                // Redirect to an error view or display an error message
                return View(null);
            }

            return View(products);
        }

        // GET: ProductController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Product product;

            // Make a GET request to the Web API's Get method
            HttpResponseMessage response = await _httpClient.GetAsync($"/api/product/{id}");

            if (response.IsSuccessStatusCode)
            {
                // Read the response content
                string responseContent = await response.Content.ReadAsStringAsync();

                // Deserialize the response content to a product
                product = JsonConvert.DeserializeObject<Product>(responseContent);

                return View(product);
            }
            else
            {
                // Handle the error response
                // Redirect to an error view or display an error message
                return View();
            }
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Make a POST request to the Web API's CreateProduct method
                    HttpResponseMessage response = await _httpClient.PostAsJsonAsync("/api/product", product);

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
                else
                {
                    // Invalid product data
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
        public async Task<ActionResult> Edit(int id)
        {
            Product product = new Product();

            // Make a GET request to the Web API's Get method with the product ID
            HttpResponseMessage response = await _httpClient.GetAsync($"/api/product/{id}");

            if (response.IsSuccessStatusCode)
            {
                // Read the response content
                string responseContent = await response.Content.ReadAsStringAsync();

                // Deserialize the response content to a product object
                product = JsonConvert.DeserializeObject<Product>(responseContent);
            }
            else
            {
                // Handle the error response
                // Redirect to an error view or display an error message
                return View();
            }

            return View(product);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditProduct(int id, Product product)
        {
            ProductDetails productDetails = new ProductDetails
            {
                Id = id,
                Name = product.Name,
                Quantity = product.Quantity,
                WarehouseId = product.WarehouseId
            };
            try
            {
                // Serialize the product object to JSON
                string jsonProduct = JsonConvert.SerializeObject(productDetails);

                // Create a StringContent with the JSON data
                var content = new StringContent(jsonProduct, Encoding.UTF8, "application/json");

                // Make a PUT request to the Web API's UpdateProduct method with the product ID
                HttpResponseMessage response = await _httpClient.PutAsync($"/api/product/{id}", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Handle the error response
                    // Redirect to an error view or display an error message
                    return RedirectToAction("Edit", new { id = id });
                }
            }
            catch
            {
                // Exception occurred
                // Redirect to an error view or display an error message
                return RedirectToAction("Edit", new { id = id });
            }
        }

        // GET: ProductController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            Product product;

            // Make a GET request to the Web API's Get method with the product ID
            HttpResponseMessage response = await _httpClient.GetAsync($"/api/product/{id}");

            if (response.IsSuccessStatusCode)
            {
                // Read the response content
                string responseContent = await response.Content.ReadAsStringAsync();

                // Deserialize the response content to a product object
                product = JsonConvert.DeserializeObject<Product>(responseContent);
            }
            else
            {
                // Handle the error response
                // Redirect to an error view or display an error message
                return View();
            }

            return View(product);
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<ActionResult> DeletePost(int id)
        {
            // Make a DELETE request to the Web API's DeleteProduct method with the product ID
            HttpResponseMessage response = await _httpClient.DeleteAsync($"/api/product/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                // Handle the error response
                // Redirect to an error view or display an error message
                return View();
            }
        }

    }
}

