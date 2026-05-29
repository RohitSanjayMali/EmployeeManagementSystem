using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _deptRepo;

        public DepartmentController(IDepartmentRepository deptRepo)
        {
            _deptRepo = deptRepo;
        }

        public async Task<IActionResult> Index()
        {
            var departments = await _deptRepo.GetAllAsync();
            return View(departments);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Department model)
        {
            if (!ModelState.IsValid) return View(model);
            await _deptRepo.AddAsync(model);
            TempData["Success"] = "Department added successfully!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var dept = await _deptRepo.GetByIdAsync(id);
            if (dept == null) return NotFound();
            return View(dept);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Department model)
        {
            if (!ModelState.IsValid) return View(model);
            await _deptRepo.UpdateAsync(model);
            TempData["Success"] = "Department updated successfully!";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _deptRepo.DeleteAsync(id);
            return Json(new { success = true, message = "Department deleted!" });
        }
    }
}