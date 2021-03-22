using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using VogCodeChallenge.Entities;

namespace VogCodeChallenge.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly List<Department> _departments;
        private readonly List<Employee> _employees;

        public IEnumerable GetAll()
        {
            throw new NotImplementedException();
        }

        public IList ListAll()
        {
            throw new NotImplementedException();
        }

        private List<Department> CreateSampleDepartments()
        {
            return new List<Department>
            {
                new Department{ Id = 1, Name ="Engineering", Address = "Developers Avenue"},
                new Department{ Id=2, Name="Sales", Address="Sales Drive"}
            };
        }
    }
}
