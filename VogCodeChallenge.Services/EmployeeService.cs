using System.Collections;
using System.Collections.Generic;
using System.Linq;
using VogCodeChallenge.Entities;

namespace VogCodeChallenge.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly List<Department> _departments;
        private readonly List<Employee> _employees;

        public EmployeeService()
        {
            _departments = CreateSampleDepartments();
            _employees = CreateSampleEmployees(_departments);
        }

        public EmployeeService(List<Employee> employees, List<Department> departments)
        {
            _departments = departments;
            _employees = employees;
        }

        public IEnumerable GetAll()
        {
            return Query();
        }

        public IList ListAll()
        {
            return Query().ToList();
        }

        private IEnumerable<Employee> Query()
        {
            return from employee in _employees
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
                   };
        }

        private List<Department> CreateSampleDepartments()
        {
            return new List<Department>
            {
                new Department{ Id = 1, Name ="Engineering", Address = "Developers Avenue"},
                new Department{ Id=2, Name="Sales", Address="Sales Drive"}
            };
        }

        private List<Employee> CreateSampleEmployees(List<Department> departments)
        {
            return new List<Employee>
            {
                new Employee
                {
                    Id =1, FirstName = "Emem", LastName="Isaac", JobTitle="Software Engineer", Address="4th Avenue", DepartmentId = departments.First().Id
                },
                new Employee
                {
                    Id =1, FirstName = "Kim", LastName="Doran", JobTitle="Software Engineer", Address="5th Avenue", DepartmentId = departments.First().Id
                },
                new Employee
                {
                    Id =1, FirstName = "Kim", LastName="Miran", JobTitle="Sales Executive", Address="6th Avenue", DepartmentId = departments.Last().Id
                },
            };
        }

        public IList<Employee> GetEmployeesByDepartment(int departmentId)
        {
            return Query().Where(employee => employee.DepartmentId == departmentId).ToList();
        }
    }
}
