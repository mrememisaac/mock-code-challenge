using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public IEnumerable GetAll()
        {
            throw new NotImplementedException();
        }

        public EmployeesApiViewModel GetAll(int page, int recordsPerPage)
        {
            throw new NotImplementedException();
        }

        public IList<Employee> GetEmployeesByDepartment(int departmentId)
        {
            throw new NotImplementedException();
        }

        public IList ListAll()
        {
            throw new NotImplementedException();
        }
    }
}
