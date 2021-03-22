using Microsoft.EntityFrameworkCore;
using VogCodeChallenge.Entities;

namespace VogCodeChallenge.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

        //make the deparment address property unique
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>().HasAlternateKey(d => d.Address);
        }

    }
}
