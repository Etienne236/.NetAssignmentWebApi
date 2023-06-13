using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi1.Models
{
    public class SalaryLog
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("EmployeeId")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public int OldSalary { get; set; }
        public int NewSalary { get; set; }
        public DateTime LastModified { get; set; }
    }
}
