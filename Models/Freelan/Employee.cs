using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcFreelan.Models.Freelan
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        [Column(TypeName = "varchar(150)")]
        [Required]
        [StringLength(150)]
        [Unicode(false)]
        public string EmployeeName { get; set; }

        [Column(TypeName = "varchar(70)")]
        [StringLength(70)]
        [Unicode(false)]
        public string EmployeeDepartment { get; set; }

        [Column(TypeName = "varchar(70)")]
        [StringLength(70)]
        [Unicode(false)]
        public string EmployeeEmail { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal EmployeeSalary { get; set; }
    }
}
