# Test Project

Contains test for the methods in the employee service

# Can_list_all_employees Test
Verifies that the EmployeeService indeed returns a list of employees both as an IList and as an IEnumerable.
```
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
```

The sample data methods help create test data

## Department test data
```
    private List<Department> CreateDepartmentSampleData()
    {
        return new List<Department>
        {
            new Department{ Id = 1, Name ="Engineering", Address = "Developers Avenue"},
            new Department{ Id=2, Name="Sales", Address="Sales Drive"}
        };
    }
```

## Employee test data
```
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
```
