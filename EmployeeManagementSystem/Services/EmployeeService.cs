using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Repositories;
using EmployeeManagementSystem.ViewModels;

namespace EmployeeManagementSystem.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepo;
        private readonly IDepartmentRepository _departmentRepo;
        private readonly ILogger<EmployeeService> _logger;
        public EmployeeService(IEmployeeRepository employeeRepo, IDepartmentRepository departmentRepo, ILogger<EmployeeService> logger)
        {
            _employeeRepo = employeeRepo;
            _departmentRepo = departmentRepo;
            _logger = logger;
        }

        public async Task<EmployeeListViewModel> GetPagedEmployeesAsync(string? searchTerm, int? departmentId, int page, int pageSize)
        {
            var (employees, totalCount) = await _employeeRepo.GetPagedAsync(searchTerm, departmentId, page, pageSize);
            var departments = await _departmentRepo.GetAllAsync();

            return new EmployeeListViewModel
            {
                Employees = employees.ToList(),
                Departments = departments.Select(d => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = d.Name
                }).ToList(),
                SearchTerm = searchTerm,
                DepartmentFilter = departmentId,
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling((double)totalCount / pageSize),
                PageSize = pageSize
            };
        }

        public async Task<Employee?> GetByIdAsync(int id) => await _employeeRepo.GetByIdAsync(id);

        public async Task AddAsync(EmployeeViewModel model, string webRootPath)
        {
            var employee = new Employee
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Phone = model.Phone,
                Position = model.Position,
                Salary = model.Salary,
                DepartmentId = model.DepartmentId,
                JoiningDate = model.JoiningDate,
                IsActive = model.IsActive
            };

            if (model.ImageFile != null)
                employee.ProfileImage = await SaveImageAsync(model.ImageFile, webRootPath);

            await _employeeRepo.AddAsync(employee);
            _logger.LogInformation("Employee {Name} added", employee.FullName);
        }

        public async Task UpdateAsync(EmployeeViewModel model, string webRootPath)
        {
            var employee = await _employeeRepo.GetByIdAsync(model.Id);
            if (employee == null) return;

            employee.FirstName = model.FirstName;
            employee.LastName = model.LastName;
            employee.Email = model.Email;
            employee.Phone = model.Phone;
            employee.Position = model.Position;
            employee.Salary = model.Salary;
            employee.DepartmentId = model.DepartmentId;
            employee.JoiningDate = model.JoiningDate;
            employee.IsActive = model.IsActive;

            if (model.ImageFile != null)
                employee.ProfileImage = await SaveImageAsync(model.ImageFile, webRootPath);

            await _employeeRepo.UpdateAsync(employee);
            _logger.LogInformation("Employee {Name} updated", employee.FullName);
        }

        public async Task DeleteAsync(int id)
        {
            _logger.LogInformation("Deleting employee {Id}", id);
            await _employeeRepo.DeleteAsync(id);
        }

        public async Task<DashboardViewModel> GetDashboardDataAsync()
        {
            var employees = await _employeeRepo.GetAllAsync();
            var departments = await _departmentRepo.GetAllAsync();
            var employeeList = employees.ToList();

            return new DashboardViewModel
            {
                TotalEmployees = employeeList.Count,
                TotalDepartments = departments.Count(),
                ActiveEmployees = employeeList.Count(e => e.IsActive),
                TotalSalary = employeeList.Sum(e => e.Salary),
                RecentEmployees = employeeList.Take(5).ToList(),
                DepartmentStats = departments.Select(d => new DepartmentStats
                {
                    DepartmentName = d.Name,
                    EmployeeCount = d.Employees.Count
                }).ToList()
            };
        }

        public async Task<byte[]> ExportToPdfAsync()
        {
            var employees = await _employeeRepo.GetAllAsync();
            // Simple PDF export using basic byte array
            // In production, use QuestPDF
            var content = "Employee Report\n\n";
            foreach (var e in employees)
                content += $"{e.FullName} | {e.Email} | {e.Department?.Name} | {e.Position}\n";

            return System.Text.Encoding.UTF8.GetBytes(content);
        }

        private async Task<string> SaveImageAsync(IFormFile file, string webRootPath)
        {
            var uploadsFolder = Path.Combine(webRootPath, "images", "employees");
            Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);

            return $"/images/employees/{uniqueFileName}";
        }
    }
}
