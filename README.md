# Branches

## step-6
Imagine we are connected to the Database now. We�d like to switch from in Memory implementation of Employee service to the database implementation. Suggest and apply a way to switch from your previous implementation to the new one and consider the methods you implemented before. IEnumerable<Employee> GetAll() and IList<Employee> ListAll() ** Database and Entity Framework implementation is not required

### Suggestion
Since we used an interface. We just need a new class that implements that interface but unlike the current one, 
this class will contain logic for connecting to a real database. Once that class is ready, we just need to update 
the startup class of the API project, telling it to use the new class (EFEmployeeService) instead of the old one (EmployeeService)

For reference, here is the IEmployeeService interface
```
    public interface IEmployeeService
    {
        IEnumerable GetAll();

        IList ListAll();

        IList<Employee> GetEmployeesByDepartment(int departmentId);

        EmployeesApiViewModel GetAll(int page, int recordsPerPage);
    }
```

For reference, here is the in-memory EmployeeService (the old one)
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

        public EmployeesApiViewModel GetAll(int page = 0, int recordsPerPage = 50)
        {
            //we dont want to send more than 250 records at time, TODO: make this configurable
            recordsPerPage = Math.Min(recordsPerPage, 250);
            return new EmployeesApiViewModel
            {
                Data = Query().Skip(page * recordsPerPage).Take(recordsPerPage).ToList(),
                Page = page,
                RecordsPerPage=recordsPerPage, 
                TotalRecordCount = _employees.Count
            };
        }
    }
```

And finally, here is the new one (EFEmployeeService), that uses a database via EF. The EF prefix in the class name means Entity Framework.
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

### Application

Question 6 said "sugget and apply". The apply part involved the creation of a Data Project. See it's [README](./VogCodeChallenge.Data/README.md) for more information. Another chunk of the "apply" is detailed in step-6 of the [Service Project README](./VogCodeChallenge.Services) where I create a new implementation of IEmployeeService.

To complete the switch to using ab EF database we would have to add a connection string to the appsettings.json file in the API project

```	
	"ConnectionStrings": {
		"AppDbConnection": "Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog={YourDatabaseNameHere}"
	}
```
and inject the AppDbContext in the configure services method of the startup class of the API project, 
```	
	services.AddDbContext<AppDbContext>(options => 
		options.UseSqlServer(Configuration.GetConnectionString("AppDbConnection")));
```


## step-5
Added docker support and docker compose support

## step-4

### Changes to the API Project
Created an Employees API Controller which provide the requested endpoints.
- GET	api/employees
- GET	api/employees/department/{departmentId}
and configured the API project to source an IEmployeeService from EmployeeService. For more information, See the section titled [Changes in step 4](./VogCodeChallenge.API/README.md)

### Changes to the Service Project
Created a new method for getting employees by department and another for getting a paged set of employees. For more information, See the section titled [Changes in step 4](./VogCodeChallenge.Services/README.md)

### Changes to the Test Project
Added a test that verifies that the service can filter employees by department and another that verifies that the service can provide a paged list of employees. For more information, See the section titled [Changes in step 4](./VogCodeChallenge.Tests/README.md)

## step-3
Created a class library [Service Project](./VogCodeChallenge.Services/README.md) to house our Services and an [xUnit Test Project](./VogCodeChallenge.Tests/README.md) to verify Service behaviour. 

## step-2
Created the Department and Employee entities. For more information please see the [Entities Project readme](./VogCodeChallenge.Entities/README.md)

## step-1
Created a blank solution and added a .NET Web API Project