# Description
This is the WEB API component of the project. Where the API Controllers live.

## step-4
The default Get action calls  GetAll(page, recordsPerPage) of the EmployeeService for scalability reasons. Similarly the call to GetEmployeeByDepartment in the action that handles the employees/department/departmentId route, will ensure we do the querying at the database level. I also added Swagger API documentation available at https://localhost:44353/swagger/AppOpenAPISpecification/swagger.json


### Employees Controller
```
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ILogger<EmployeesController> _logger;
        private readonly IEmployeeService _employeeService;

        public EmployeesController(ILogger<EmployeesController> logger, IEmployeeService employeeService)
        {
            _logger = logger;
            _employeeService = employeeService;
        }

        private ObjectResult ServerError(string message = "Service unreachable")
        {
            return StatusCode(StatusCodes.Status500InternalServerError, message);
        }

        [HttpGet()]
        public ActionResult Get(int page, int recordsPerPage = 50)
        {
            try
            {
                return Ok(_employeeService.GetAll(page, recordsPerPage));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                return ServerError();
            }
        }


        [HttpGet("department/{departmentId:int}")]
        public ActionResult<Employee> Get(int departmentId)
        {
            try
            {
                return Ok(_employeeService.GetEmployeesByDepartment(departmentId));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                return ServerError();
            }
        }
    }
```

### Startup changes
```
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<IEmployeeService, EmployeeService>();
    }
```