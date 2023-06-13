using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Warehouse
    {
        [Key]
        public int WarehouseId { get; set; }
        [Required]
        public string Name { get; set; }

    }
}
