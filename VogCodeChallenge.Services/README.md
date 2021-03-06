# Service Project
The EmployeeService, EFEmployeeService and the IEmployeeService live here

## step-6

The following changes are required to fulfill the requirements in step-6

1. Reference the VogCodeChallenge.Data project from the VogCodeChallenge.Services project
2. Create a new EmployeeService class, let's call it EFEmployeeService - the EF meaning Entity Framework. It must implement the interface IEmployee
3. Add a readonly field of type DbSet<Employee>. We'll call it _employees.
3. Add a readonly field of type DbSet<Departments>. We'll call it _departments.
4. Provide a constructor that accepts a dbContext
5. In the constructor method, do this
    - _departments = dbContext.Departments
    - _employees = dbContext.Employees
    - Create a Query() method with a return type of Task<IQueryAble<Employee>>
    - Create a GetAll() method that returns Query().Result.AsEnumerable();
    - Create a ListAll() method that returns Query().Result.ToList();
    - Create a GetEmployeesByDepartment method that returns Task<List<Employee>>
    - Create a GetAll(page, recordsPerpage) method that returns Task<EmployeeApiViewModel>

Now that we will be working databases, I have modified the IEmployee interface and Employee Service to return Task types, resulting in v4.1, v5.1

### EFEmployeeService
```
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
```

## step-4

### New Interface Methods

For filtering employees by department GetEmployeesByDepartment(int departmentId) and for getting paged set of employees  GetAll(int page, int recordsPerPage). Here is our updated interface
```
     public interface IEmployeeService
    {
        IEnumerable GetAll();

        IList ListAll();

        IList<Employee> GetEmployeesByDepartment(int departmentId);
        IList<Employee> GetAll(int page, int recordsPerPage);
    }
```

### Changes to the EmployeeService class that implement the above

```
    public Task<List<Employee>> GetEmployeesByDepartment(int departmentId)
    {
        var result = Task
            .Run(
                () => Query().Where(employee => employee.DepartmentId == departmentId).ToList()
            );
        return result;
    }

    public Task<EmployeesApiViewModel> GetAll(int page = 0, int recordsPerPage = 50)
    {
        //we dont want to send more than 250 records at time, TODO: make this configurable
        recordsPerPage = Math.Min(recordsPerPage, 250);
        return Task.FromResult(new EmployeesApiViewModel
        {
            Data = Query().Skip(page * recordsPerPage).Take(recordsPerPage).ToList(),
            Page = page,
            RecordsPerPage=recordsPerPage, 
            TotalRecordCount = _employees.Count
        });
    }
```

#step 3

## IEmployeeService interface
This is what the looks like

```
    public interface IEmployeeService
    {
        IEnumerable GetAll();

        IList ListAll();
    }
```

## EmployeeService class

The parameterized constructor is used by the [Employee Service Test class](../VogCodeChallenge.Tests/README.md)

```
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
    }
```

The remaining methods are for generating sample data

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

## step-3
Created the services project. And the Employee Service class and its interface. Also added a custom helper method - Query to do the heavy lifting for GetAll and ListAll. That way, we reduce code duplication.
