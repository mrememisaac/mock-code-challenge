using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VogCodeChallenge.Data;
using VogCodeChallenge.Entities;

namespace VogCodeChallenge.Services
{
    public class EFEmployeeService : IEmployeeService
    {
        private readonly DbSet<Employee> _employees;
        private readonly DbSet<Department> _departments;

        public EFEmployeeService(AppDbContext context)
        {
            _employees = context.Employees;
            _departments = context.Departments;
        }

        private Task<IQueryable<Employee>> Query()
        {
            return Task.Run(() => from employee in _employees
                   join department in _departments
                   on employee.DepartmentId equals department.Id
                   select new Employee
                   {
                       Id = employee.Id,
                       DepartmentId = employee.DepartmentId,
                       FirstName = employee.FirstName,
                       LastName = employee.LastName,
                       JobTitle = employee.JobTitle,
                       Address = employee.Address,
                       Department = department
                   });
        }

        public IEnumerable GetAll()
        {
            return Query().Result.AsEnumerable();
        }

        public Task<EmployeesApiViewModel> GetAll(int page = 0, int recordsPerPage = 50)
        {
            //we dont want to send more than 250 records at time, TODO: make this configurable
            recordsPerPage = Math.Min(recordsPerPage, 250);
            return Task.FromResult(new EmployeesApiViewModel
            {
                Data = Query().Result.Skip(page * recordsPerPage).Take(recordsPerPage).ToList(),
                Page = page,
                RecordsPerPage = recordsPerPage,
                TotalRecordCount = _employees.Count()
            });
        }

        public Task<List<Employee>> GetEmployeesByDepartment(int departmentId)
        {
            var result = Task
                .Run(
                    () => Query().Result.Where(employee => employee.DepartmentId == departmentId).ToList()
                );
            return result;
        }

        public IList ListAll()
        {
            return Query().Result.ToList();
        }
    }
}
