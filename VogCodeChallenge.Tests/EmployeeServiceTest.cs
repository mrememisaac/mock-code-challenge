using System.Collections.Generic;
using VogCodeChallenge.Entities;

namespace VogCodeChallenge.Tests
{
    public class EmployeeServiceTest
    {
        private List<Department> CreateDepartmentSampleData()
        {
            return new List<Department>
            {
                new Department{ Id = 1, Name ="Engineering", Address = "Developers Avenue"},
                new Department{ Id=2, Name="Sales", Address="Sales Drive"}
            };
        }

    }
}
