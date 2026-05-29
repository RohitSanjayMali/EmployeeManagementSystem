using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
            => await _context.Employees.Include(e => e.Department).OrderByDescending(e => e.Id).ToListAsync();

        public async Task<Employee?> GetByIdAsync(int id)
            => await _context.Employees.Include(e => e.Department).FirstOrDefaultAsync(e => e.Id == id);

        public async Task AddAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Employee employee)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Employee>> SearchAsync(string? searchTerm, int? departmentId)
        {
            var query = _context.Employees.Include(e => e.Department).AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
                query = query.Where(e => e.FirstName.Contains(searchTerm) ||
                                        e.LastName.Contains(searchTerm) ||
                                        e.Email.Contains(searchTerm));
            if (departmentId.HasValue)
                query = query.Where(e => e.DepartmentId == departmentId);

            return await query.OrderByDescending(e => e.Id).ToListAsync();
        }

        public async Task<(IEnumerable<Employee> employees, int totalCount)> GetPagedAsync(
            string? searchTerm, int? departmentId, int page, int pageSize)
        {
            var query = _context.Employees.Include(e => e.Department).AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
                query = query.Where(e => e.FirstName.Contains(searchTerm) ||
                                        e.LastName.Contains(searchTerm) ||
                                        e.Email.Contains(searchTerm));

            if (departmentId.HasValue)
                query = query.Where(e => e.DepartmentId == departmentId);

            var totalCount = await query.CountAsync();
            var employees = await query.OrderByDescending(e => e.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (employees, totalCount);
        }
    }
}
