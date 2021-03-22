# Branches

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