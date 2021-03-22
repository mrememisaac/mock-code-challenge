# Project Description
The data access logic lives here

## step-6
The following changes were made because of "suggest and apply" part of Task 6. This is part of the "apply"

1.    Add a new .NET Core 3.1 class library project  - VogCodeChallenge.Data and
2.    Install Microsoft.EntityFrameworkCore.SqlServer so we can interact with MSSQL via entity fromework and
3.    Install Microsoft.EntityFrameworkCore.Tools & Microsoft.EntityFrameworkCore.Design for access to migration commands and apis
4.    Reference the VogCodeChallenge.Entities project from the VogCodeChallenge.Data project
5.    Create a DbContext class with DbSet<T> properties for Employee and Department entities

### AppDbContext class
	```
	public class AppDbContext : DbContext {

		public DbSet<Employee> Employees {get;set;}
		public DbSet<Department> Departments {get;set;}

		//make the deparment address property unique
		protected override void OnModelCreating(ModelBuilder modelBuilder){
			modelBuilder.Entity<Department>().HasAlternateKey(d => d.Address);
		}
	}
	```