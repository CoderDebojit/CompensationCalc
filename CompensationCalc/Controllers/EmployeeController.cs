using CompensationCalc.Model;
using CompensationCalc.Service;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CompensationCalc.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly IEmployeeService _employeeService;
        
        public EmployeeController(ILogger<EmployeeController> logger,
            IEmployeeService employeeService)
        {
            _logger = logger;
            _employeeService = employeeService;
        }

        [HttpGet("~/details")]
        public async Task<IActionResult> GetEmployeeDetails([FromQuery] int employeeCode)
        {
            var (result, statusCode) = await _employeeService.GetEmployeeCompensation(employeeCode);

            if(statusCode == HttpStatusCode.OK)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(new { Details = "No employee found, please insert valid employeeCode"});
            }
        }

        [HttpPost("~/details")]
        public async Task<IActionResult> CreateEmployeeDetails([FromBody] Employee request)
        {
            var (result, statusCode) = await _employeeService.CreateEmployee(request);

            if (statusCode == HttpStatusCode.Created)
            {
                return Created(nameof(result), result);
            }
            else
            {
                return BadRequest(new { Details = "Employee already exist" });
            }
        }

        [HttpPut("~/details")]
        public async Task<IActionResult> UpdateEmployeeDetails([FromBody] Employee request)
        {
            var (result, statusCode) = await _employeeService.UpdateEmployee(request);

            if (statusCode == HttpStatusCode.OK)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(new { Details = "No employee found, please insert valid employeeCode" });
            }
        }
    }
}