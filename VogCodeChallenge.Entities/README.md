# Entities Project
All Core Domain entities live here. If this were a micro service, this might not be necessary as clones of 
entities with applicable variations would exist in each project

## step-3
Added a view model for the api
```
    public class EmployeesApiViewModel
    {
        public int TotalRecordCount { get; set; }

        public int Page { get; set; }

        public int RecordsPerPage { get; set; }

        public List<Employee> Data { get; set; }
    }
```
## step-2
Added Department and Employee entities, using the EntityBase class made it unnecessary to duplicate properties common
to both entities