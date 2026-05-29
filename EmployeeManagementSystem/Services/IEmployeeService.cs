using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.ViewModels;

namespace EmployeeManagementSystem.Services
{
    public interface IEmployeeService
    {
        Task<EmployeeListViewModel> GetPagedEmployeesAsync(string? searchTerm, int? departmentId, int page, int pageSize);
        Task<Employee?> GetByIdAsync(int id);
        Task AddAsync(EmployeeViewModel model, string webRootPath);
        Task UpdateAsync(EmployeeViewModel model, string webRootPath);
        Task DeleteAsync(int id);
        Task<DashboardViewModel> GetDashboardDataAsync();
        Task<byte[]> ExportToPdfAsync();
    }
}
