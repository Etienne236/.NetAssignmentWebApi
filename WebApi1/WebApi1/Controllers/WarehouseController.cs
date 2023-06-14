using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApi1.Data;
using WebApi1.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi1.Controllers
{
    [Route("api/warehouse")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {

        private readonly MyDbContext _dbContext;

        public WarehouseController(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> ExecuteProcedure()
        {
            var result = await _dbContext.Warehouses.FromSqlRaw("EXECUTE dbo.SelectAllFromTable @TableName",
        new SqlParameter("@TableName", "Warehouses"))
        .ToListAsync();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var warehouse = _dbContext.Warehouses.SingleOrDefault(x => x.Id == id);

            if (warehouse != null)
            {
                return Ok(warehouse);
            }

            return BadRequest("No Warehouse Available"); // or return any default value if the warehouse is not found
        }

        [HttpPost]
        public async Task<IActionResult> CreateWarehouse([FromBody] Warehouse warehouse)
        {
            if (warehouse == null)
            {
                return BadRequest("Product data is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _dbContext.Database.ExecuteSqlRawAsync("EXECUTE dbo.InsertWarehouse @Name",new SqlParameter("@Name", warehouse.Name));

            return Ok("Warehouse created successfully.");
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWarehouse(int id, [FromBody] Warehouse warehouse)
        {
            if (warehouse == null)
            {
                return BadRequest("Product data is null.");
            }

            if (id != warehouse.Id)
            {
                return BadRequest("Warehouse ID mismatch.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@Id", id),
        new SqlParameter("@Name", warehouse.Name)
            };

            await _dbContext.Database.ExecuteSqlRawAsync("EXECUTE dbo.UpdateWarehouse @Id, @Name", parameters);

            return Ok("Product updated successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWarehouse(int id)
        {
            SqlParameter parameter = new SqlParameter("@Id", id);

            await _dbContext.Database.ExecuteSqlRawAsync("EXECUTE dbo.DeleteWarehouse @Id", parameter);

            return Ok("Warehouse deleted successfully.");
        }

    }
}
