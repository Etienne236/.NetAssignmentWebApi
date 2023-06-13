using System.ComponentModel.DataAnnotations;

namespace WebApi1.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Salary { get; set; }
    }
}
