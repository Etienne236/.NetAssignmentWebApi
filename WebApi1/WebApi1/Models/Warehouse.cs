using System.ComponentModel.DataAnnotations;

namespace WebApi1.Models
{
    public class Warehouse
    {
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }

}
}
