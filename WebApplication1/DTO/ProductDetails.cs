using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTO
{
    public class ProductDetails
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public int WarehouseId { get; set; }
    }
}
