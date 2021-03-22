﻿using System.Collections.Generic;
using System.Linq;
using VogCodeChallenge.Entities;
using VogCodeChallenge.Services;
using Xunit;

namespace VogCodeChallenge.Tests
{
    public class EmployeeServiceTest
    {

        [Fact]
        public void Can_list_all_employees()
        {
            var departments = CreateDepartmentSampleData();
            var employees = CreateEmployeeSampleData(departments);

            IEmployeeService sut = new EmployeeService(employees, departments);
            var iEnumerableResult = sut.GetAll();
            var iListResult = sut.ListAll();

            Assert.Equal(employees, iEnumerableResult);
            Assert.Equal(employees, iListResult);
            Assert.Equal(employees.Count, iListResult.Count);
        }

        private List<Department> CreateDepartmentSampleData()
        {
            return new List<Department>
            {
                new Department{ Id = 1, Name ="Engineering", Address = "Developers Avenue"},
                new Department{ Id=2, Name="Sales", Address="Sales Drive"}
            };
        }

        private List<Employee> CreateEmployeeSampleData(List<Department> departments)
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
    }
}
