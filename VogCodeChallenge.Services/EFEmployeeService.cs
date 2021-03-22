using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VogCodeChallenge.Data;
using VogCodeChallenge.Entities;

namespace VogCodeChallenge.Services
{
    public class EFEmployeeService
    {
        private readonly DbSet<Employee> _employees;
        private readonly DbSet<Department> _departments;

        public EFEmployeeService(AppDbContext context)
        {
            _employees = context.Employees;
            _departments = context.Departments;
        }
    }
}
