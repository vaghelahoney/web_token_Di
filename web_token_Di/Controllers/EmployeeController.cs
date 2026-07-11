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

        public EmployeeController(IEmployeeRepositories repository)
        {
            _repository = repository;
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
            var employee = await _repository.GetEmployeeByIdAsync(id);
            if (employee == null) return NotFound();
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
