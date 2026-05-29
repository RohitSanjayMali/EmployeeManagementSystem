using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.Models
{
    public class Department
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Department Name is Required")]
        [StringLength(100)]
        public string Name { get; set; }

        public string? Description { get; set; }

        public DateTime CreateDate { get; set; }
        public bool IsActive { get; set; }

        // Navigation property for related employees

        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
