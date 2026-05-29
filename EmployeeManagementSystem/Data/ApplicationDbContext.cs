using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){ }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Department>().HasData(
                new Department { Id = 1, Name = "IT", Description = "Information Technology", CreateDate = new DateTime(2024, 1, 1) },
                new Department { Id = 2, Name = "HR", Description = "Human Resources", CreateDate = new DateTime(2024, 1, 1) },
                new Department { Id = 3, Name = "Finance", Description = "Finance & Accounts", CreateDate = new DateTime(2024, 1, 1) },
                new Department { Id = 4, Name = "Marketing", Description = "Marketing & Sales", CreateDate = new DateTime(2024, 1, 1) },
                new Department { Id = 5, Name = "Operations", Description = "Operations & Logistics", CreateDate = new DateTime(2024, 1, 1) }
            );
        }
    }
}
