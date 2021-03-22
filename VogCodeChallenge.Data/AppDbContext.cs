using Microsoft.EntityFrameworkCore;
using VogCodeChallenge.Entities;

namespace VogCodeChallenge.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

    }
}
