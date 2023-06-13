using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApi1.Data;
using WebApi1.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi1.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly MyDbContext _dbContext;

        public ProductController(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> ExecuteProcedure()
        {
            var result = await _dbContext.Products.FromSqlRaw("EXECUTE dbo.SelectAllFromTable @TableName",
        new SqlParameter("@TableName", "Products"))
        .ToListAsync();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return _dbContext.Products.FirstOrDefaultAsync(x => x.Id == id).ToString();
        }

        // POST api/<ProductController>
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest("Product data is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@Name", product.Name),
        new SqlParameter("@Quantity", product.Quantity),
        new SqlParameter("@Warehouse", product.Warehouse)
            };

            await _dbContext.Database.ExecuteSqlRawAsync("EXECUTE dbo.InsertProduct @Name, @Quantity, @Warehouse", parameters);

            return Ok("Product created successfully.");
        }


        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest("Product data is null.");
            }

            if (id != product.Id)
            {
                return BadRequest("Product ID mismatch.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@Id", id),
        new SqlParameter("@Name", product.Name),
        new SqlParameter("@Quantity", product.Quantity),
        new SqlParameter("@Warehouse", product.Warehouse)
            };

            await _dbContext.Database.ExecuteSqlRawAsync("EXECUTE dbo.UpdateProduct @Id, @Name, @Quantity, @Warehouse", parameters);

            return Ok("Product updated successfully.");
        }


        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            SqlParameter parameter = new SqlParameter("@Id", id);

            await _dbContext.Database.ExecuteSqlRawAsync("EXECUTE dbo.DeleteProduct @Id", parameter);

            return Ok("Product deleted successfully.");
        }

    }
}
