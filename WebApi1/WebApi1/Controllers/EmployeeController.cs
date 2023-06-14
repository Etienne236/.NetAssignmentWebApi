using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebApi1.Data;
using WebApi1.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi1.Controllers
{
    [Route("api/employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly MyDbContext _dbContext;

        public EmployeeController(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> ExecuteProcedure()
        {
            var result = await _dbContext.Employees.FromSqlRaw("EXECUTE dbo.SelectAllFromTable @TableName",
        new SqlParameter("@TableName", "Employees"))
        .ToListAsync();

            return Ok(result);
        }

        // GET api/<EmployeeController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var employee = _dbContext.Employees.FirstOrDefault(x => x.Id == id);

            if (employee != null)
            {
                return Ok(employee);
            }

            return BadRequest("No Employees Available"); // or return any default value if the warehouse is not found
        }

        // POST api/<EmployeeController>
        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] Employee employee)
        {
            if (employee == null)
            {
                return BadRequest("Product data is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }



            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@Name", employee.Name),
        new SqlParameter("@Salary", employee.Salary)
            };

            await _dbContext.Database.ExecuteSqlRawAsync("EXECUTE dbo.InsertEmployee @Name, @Salary", parameters);

            return Ok("Employee created successfully.");
        }

        // PUT api/<EmployeeController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] Employee employee)
        {
            if (employee == null)
            {
                return BadRequest("Employee data is null.");
            }

            if (id != employee.Id)
            {
                return BadRequest("Employee ID mismatch.");
            }

            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@Id", id),
        new SqlParameter("@Name", employee.Name),
        new SqlParameter("@Salary", employee.Salary)
            };

            await _dbContext.Database.ExecuteSqlRawAsync("EXECUTE dbo.UpdateEmployee @Id, @Name, @Salary", parameters);

            return Ok("Employee updated successfully.");
        }
    }
}
