using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using VogCodeChallenge.Entities;
using VogCodeChallenge.Services;

namespace VogCodeChallenge.API.Controllers
{
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
}
