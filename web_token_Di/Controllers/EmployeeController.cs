using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using web_token_Di.Models.DTOs;
using web_token_Di.Repositories;
using Microsoft.AspNetCore.RateLimiting;

namespace web_token_Di.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepositories _repository;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IEmployeeRepositories repository, ILogger<EmployeeController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [EnableRateLimiting("ApiPolicy")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _repository.GetAllEmployeeAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation("Retrieving employee with ID {EmployeeId}", id);
            if (id <= 0)
            {
                throw new ArgumentException("Employee ID must be greater than zero.", nameof(id));
            }

            var employee = await _repository.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                throw new KeyNotFoundException($"Employee with ID {id} was not found.");
            }
            return Ok(employee);
        }

        [HttpPost]
        public async Task<ActionResult> Create(EmployeeModel employee)
        {

            if (employee == null || string.IsNullOrWhiteSpace(employee.Name))
            {
                return BadRequest("Employee name cannot be empty.");
            }

            bool isDuplicate = await _repository.FindDublicateEmployeeAsync(employee.Name, employee.Id);

            if (isDuplicate)
            {
                return Conflict(new { message = $"An employee with the name '{employee.Name}' already exists." });
            }

            await _repository.AddEmployeeAsync(employee);

            return Ok(new
            {
                message = "Employee created successfully.",
                data = employee
            });
        }

        [HttpPut]
        public async Task<IActionResult> Update(EmployeeModel employee)
        {
            if (employee == null || string.IsNullOrWhiteSpace(employee.Name))
            {
                return BadRequest("Employee name cannot be empty.");
            }

            bool isDuplicate = await _repository.FindDublicateEmployeeAsync(employee.Name, employee.Id);

            if (isDuplicate)
            {
                return Conflict(new { message = $"An employee with the name '{employee.Name}' already exists." });
            }

            var data = await _repository.UpdateEmployeeAsync(employee) ? 1 : 0;
            return data == 1 ? Ok(true) : BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _repository.DeleteEmployeeAsync(id) ? 1:0 ;
            return data == 1 ? Ok(true) : BadRequest();
        }
    }
}
