using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmployeeManagementSystem.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        [MinLength(6)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }

    public class EmployeeViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Phone]
        public string Phone { get; set; } = string.Empty;

        [Required]
        public string Position { get; set; } = string.Empty;

        [Required]
        [Range(0, 10000000)]
        public decimal Salary { get; set; }

        public string? ProfileImage { get; set; }

        public IFormFile? ImageFile { get; set; }

        public DateTime JoiningDate { get; set; } = DateTime.Now;

        public bool IsActive { get; set; } = true;

        [Required(ErrorMessage = "Department is required")]
        public int DepartmentId { get; set; }

        public List<SelectListItem> Departments { get; set; } = new();
    }

    public class EmployeeListViewModel
    {
        public List<EmployeeManagementSystem.Models.Employee> Employees { get; set; } = new();
        public List<SelectListItem> Departments { get; set; } = new();
        public string? SearchTerm { get; set; }
        public int? DepartmentFilter { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; }
        public int PageSize { get; set; } = 10;
    }

    public class DashboardViewModel
    {
        public int TotalEmployees { get; set; }
        public int TotalDepartments { get; set; }
        public int ActiveEmployees { get; set; }
        public decimal TotalSalary { get; set; }
        public List<EmployeeManagementSystem.Models.Employee> RecentEmployees { get; set; } = new();
        public List<DepartmentStats> DepartmentStats { get; set; } = new();
    }

    public class DepartmentStats
    {
        public string DepartmentName { get; set; } = string.Empty;
        public int EmployeeCount { get; set; }
    }
}

//  Step 6 — Repositories banao
