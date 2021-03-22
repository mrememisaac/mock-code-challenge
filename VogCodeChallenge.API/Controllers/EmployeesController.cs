using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        [HttpGet]
        public ActionResult<List<Employee>> Get() 
        {
            try
            {
                return Ok(_employeeService.GetAll());
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                return ServerError();
            }
        }
    }
}
