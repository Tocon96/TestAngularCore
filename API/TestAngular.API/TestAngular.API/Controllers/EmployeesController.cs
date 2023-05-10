using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestAngular.API.Data;
using TestAngular.API.Models;

namespace TestAngular.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : Controller
    {
        private readonly TestAngularDbContext _context;
        public EmployeesController(TestAngularDbContext dbContext)
        {
            _context = dbContext;   
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees() 
        {
            var employees = await _context.Employees.ToListAsync();

            return Ok(employees);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] Employee employeeRequest)
        {
            employeeRequest.Id = Guid.NewGuid();
            await _context.Employees.AddAsync(employeeRequest);
            await _context.SaveChangesAsync();

            return Ok(employeeRequest);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetEmployee([FromRoute]Guid Id)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(x=> x.Id == Id);

            if(employee == null)
            {
                return BadRequest();
            }

            return Ok(employee);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpgradeEmployee([FromRoute] Guid Id, Employee updateEmployeeRequest)
        {
            var employee = await _context.Employees.FindAsync(Id);
            if(employee == null)
            {
                return NotFound();
            }

            employee.Name = updateEmployeeRequest.Name;
            employee.Email = updateEmployeeRequest.Email;  
            employee.Salary = updateEmployeeRequest.Salary;
            employee.Phone = updateEmployeeRequest.Phone;
            employee.Department = updateEmployeeRequest.Department;

            await _context.SaveChangesAsync();

            return Ok(employee);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid Id)
        {
            var employee = await _context.Employees.FindAsync(Id);

            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return Ok(employee);
        }
    }
}
