# Test Project

Contains test for the methods in the employee service

## step-4
Added two tests.

## can_filter_employees_by_department
Verifies that service can filter employees by department
```
    public void can_filter_employees_by_department(int departmentId)
    {
        var departments = CreateDepartmentSampleData();
        var employees = CreateEmployeeSampleData(departments);

        IEmployeeService sut = new EmployeeService(employees, departments);
        var result = sut.GetEmployeesByDepartment(departmentId);
        var hasEmployeeFromAnotherDepartment = result.Any(x => x.DepartmentId != departmentId);

        Assert.False(hasEmployeeFromAnotherDepartment);
    }
```

## Can_list_employees_in_pages_of_specified_size
Verifies that the service can returned a paged data set
```
    [Theory]
        [InlineData(0, 1, 1)] //page 0, records 1, count should be 1
        [InlineData(0, 2, 2)] //page 0, records 2, count should be 2
        [InlineData(0, 3, 3)] //page 0, records 3, count should be 3
        [InlineData(1, 3, 0)] //page 1, records 3, count should be 0
        [InlineData(1, 2, 1)] //page 1, records 2, count should be 1
        [InlineData(10, 2, 0)] //page 1, records 2, count should be 1
        public void Can_list_employees_in_pages_of_specified_size(int page, int recordsPerPage, int count)
        {
            var departments = CreateDepartmentSampleData();
            var employees = CreateEmployeeSampleData(departments);

            IEmployeeService sut = new EmployeeService(employees, departments);
            var result = sut.GetAll(page, recordsPerPage);

            Assert.Equal(result.Data.Count, count);
        }
```

## step-3

### Can_list_all_employees Test
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

#### Department test data
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

#### Employee test data
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
