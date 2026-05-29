using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<Employee?> GetByIdAsync(int id);
        Task AddAsync(Employee employee);
        Task UpdateAsync(Employee employee);
        Task DeleteAsync(int id);
        Task<IEnumerable<Employee>> SearchAsync(string? searchTerm, int? departmentId);
        Task<(IEnumerable<Employee> employees, int totalCount)> GetPagedAsync(string? searchTerm, int? departmentId, int page, int pageSize);
    }
}
