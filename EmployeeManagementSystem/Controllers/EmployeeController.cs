using EmployeeManagementSystem.Repositories;
using EmployeeManagementSystem.Services;
using EmployeeManagementSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmployeeManagementSystem.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentRepository _deptRepo;
        private readonly IWebHostEnvironment _env;

        public EmployeeController(IEmployeeService employeeService,
            IDepartmentRepository deptRepo,
            IWebHostEnvironment env)
        {
            _employeeService = employeeService;
            _deptRepo = deptRepo;
            _env = env;
        }

        public async Task<IActionResult> Index(string? searchTerm, int? departmentId, int page = 1)
        {
            var model = await _employeeService.GetPagedEmployeesAsync(searchTerm, departmentId, page, 10);
            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var employee = await _employeeService.GetByIdAsync(id);
            if (employee == null) return NotFound();
            return View(employee);
        }

        public async Task<IActionResult> Create()
        {
            var model = new EmployeeViewModel
            {
                Departments = await GetDepartmentListAsync()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Departments = await GetDepartmentListAsync();
                return View(model);
            }
            await _employeeService.AddAsync(model, _env.WebRootPath);
            TempData["Success"] = "Employee added successfully!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _employeeService.GetByIdAsync(id);
            if (employee == null) return NotFound();

            var model = new EmployeeViewModel
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                Phone = employee.Phone,
                Position = employee.Position,
                Salary = employee.Salary,
                ProfileImage = employee.ProfileImage,
                JoiningDate = employee.JoiningDate,
                IsActive = employee.IsActive,
                DepartmentId = employee.DepartmentId,
                Departments = await GetDepartmentListAsync()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EmployeeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Departments = await GetDepartmentListAsync();
                return View(model);
            }
            await _employeeService.UpdateAsync(model, _env.WebRootPath);
            TempData["Success"] = "Employee updated successfully!";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _employeeService.DeleteAsync(id);
            return Json(new { success = true, message = "Employee deleted successfully!" });
        }

        private async Task<List<SelectListItem>> GetDepartmentListAsync()
        {
            var departments = await _deptRepo.GetAllAsync();
            return departments.Select(d => new SelectListItem
            {
                Value = d.Id.ToString(),
                Text = d.Name
            }).ToList();
        }
    }
}