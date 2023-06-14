using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly HttpClient _httpClient;

        public EmployeeController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:44317/"); // Replace with your Web API base URL
        }

        // GET: EmployeeController
        public async Task<ActionResult> Index()
        {
            List<Employee> employees = new List<Employee>();

            // Make a GET request to the Web API's ExecuteProcedure method
            HttpResponseMessage response = await _httpClient.GetAsync("/api/employee");

            if (response.IsSuccessStatusCode)
            {
                // Read the response content
                string responseContent = await response.Content.ReadAsStringAsync();

                // Deserialize the response content to a list of employees
                employees = JsonConvert.DeserializeObject<List<Employee>>(responseContent);
            }
            else
            {
                // Handle the error response
                // Redirect to an error view or display an error message
                return View(null);
            }

            return View(employees);
        }

        // GET: EmployeeController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Employee employee;

            // Make a GET request to the Web API's Get method
            HttpResponseMessage response = await _httpClient.GetAsync($"/api/employee/{id}");

            if (response.IsSuccessStatusCode)
            {
                // Read the response content
                string responseContent = await response.Content.ReadAsStringAsync();

                // Deserialize the response content to a employee
                employee = JsonConvert.DeserializeObject<Employee>(responseContent);

                return View(employee);
            }
            else
            {
                // Handle the error response
                // Redirect to an error view or display an error message
                return View();
            }
        }

        // GET: EmployeeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmployeeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Employee employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Make a POST request to the Web API's CreateEmployee method
                    HttpResponseMessage response = await _httpClient.PostAsJsonAsync("/api/employee", employee);

                    if (response.IsSuccessStatusCode)
                    {
                        // Employee created successfully
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
                    // Invalid employee data
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

        // GET: EmployeeController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Employee employee = new Employee();

            // Make a GET request to the Web API's Get method with the employee ID
            HttpResponseMessage response = await _httpClient.GetAsync($"/api/employee/{id}");

            if (response.IsSuccessStatusCode)
            {
                // Read the response content
                string responseContent = await response.Content.ReadAsStringAsync();

                // Deserialize the response content to a employee object
                    employee = JsonConvert.DeserializeObject<Employee>(responseContent);
            }
            else
            {
                // Handle the error response
                // Redirect to an error view or display an error message
                return View();
            }

            return View(employee);
        }

        // POST: EmployeeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditEmployee(int id, Employee employee)
        {
            try
            {
                // Serialize the employee object to JSON
                string jsonEmployee = JsonConvert.SerializeObject(employee);

                // Create a StringContent with the JSON data
                var content = new StringContent(jsonEmployee, Encoding.UTF8, "application/json");

                // Make a PUT request to the Web API's UpdateEmployee method with the employee ID
                HttpResponseMessage response = await _httpClient.PutAsync($"/api/employee/{id}", content);

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
    }
}
