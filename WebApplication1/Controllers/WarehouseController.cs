using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class WarehouseController : Controller
    {
        private readonly HttpClient _httpClient;

        public WarehouseController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:44317/"); // Replace with your Web API base URL
        }
        // GET: WarehouseController
        public async Task<ActionResult> Index()
        {
            List<Warehouse> warehouses;

            // Make a GET request to the Web API's ExecuteProcedure method
            HttpResponseMessage response = await _httpClient.GetAsync("/api/warehouse");

            if (response.IsSuccessStatusCode)
            {
                // Read the response content
                string responseContent = await response.Content.ReadAsStringAsync();

                // Deserialize the response content to a list of warehouses
                warehouses = JsonConvert.DeserializeObject<List<Warehouse>>(responseContent);
            }
            else
            {
                // Handle the error response
                // Redirect to an error view or display an error message
                return View();
            }

            return View(warehouses);
        }


        // GET: WarehouseController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Warehouse warehouse;

            // Make a GET request to the Web API's Get method with the warehouse ID
            HttpResponseMessage response = await _httpClient.GetAsync($"/api/warehouse/{id}");

            if (response.IsSuccessStatusCode)
            {
                // Read the response content
                string responseContent = await response.Content.ReadAsStringAsync();

                // Deserialize the response content to a warehouse object
                warehouse = JsonConvert.DeserializeObject<Warehouse>(responseContent);
            }
            else
            {
                // Handle the error response
                // Redirect to an error view or display an error message
                return View();
            }

            return View(warehouse);
        }

        // GET: WarehouseController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WarehouseController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Warehouse warehouse)
        {
            if (ModelState.IsValid)
            {
                // Serialize the warehouse object to JSON
                string jsonWarehouse = JsonConvert.SerializeObject(warehouse);

                // Create a StringContent with the JSON data
                var content = new StringContent(jsonWarehouse, Encoding.UTF8, "application/json");

                // Make a POST request to the Web API's CreateWarehouse method
                HttpResponseMessage response = await _httpClient.PostAsync("/api/warehouse", content);

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

            return View();
        }

        // GET: WarehouseController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Warehouse warehouse;

            // Make a GET request to the Web API's Get method with the warehouse ID
            HttpResponseMessage response = await _httpClient.GetAsync($"/api/warehouse/{id}");

            if (response.IsSuccessStatusCode)
            {
                // Read the response content
                string responseContent = await response.Content.ReadAsStringAsync();

                // Deserialize the response content to a warehouse object
                warehouse = JsonConvert.DeserializeObject<Warehouse>(responseContent);
            }
            else
            {
                // Handle the error response
                // Redirect to an error view or display an error message
                return View(new Warehouse());
            }

            return View(warehouse);
        }
        
        // POST: WarehouseController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Warehouse warehouse)
        {
            if (ModelState.IsValid)
            {
                // Serialize the warehouse object to JSON
                string jsonWarehouse = JsonConvert.SerializeObject(warehouse);

                // Create a StringContent with the JSON data
                var content = new StringContent(jsonWarehouse, Encoding.UTF8, "application/json");

                // Make a PUT request to the Web API's UpdateWarehouse method with the warehouse ID
                HttpResponseMessage response = await _httpClient.PutAsync($"/api/warehouse/{id}", content);

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

            return View();
        }

        // GET: WarehouseController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            Warehouse warehouse;

            // Make a GET request to the Web API's Get method with the warehouse ID
            HttpResponseMessage response = await _httpClient.GetAsync($"/api/warehouse/{id}");

            if (response.IsSuccessStatusCode)
            {
                // Read the response content
                string responseContent = await response.Content.ReadAsStringAsync();

                // Deserialize the response content to a warehouse object
                warehouse = JsonConvert.DeserializeObject<Warehouse>(responseContent);
            }
            else
            {
                // Handle the error response
                // Redirect to an error view or display an error message
                return View();
            }

            return View(warehouse);
        }

        // POST: WarehouseController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<ActionResult> DeletePost(int id)
        {
            // Make a DELETE request to the Web API's DeleteWarehouse method with the warehouse ID
            HttpResponseMessage response = await _httpClient.DeleteAsync($"/api/warehouse/{id}");

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
