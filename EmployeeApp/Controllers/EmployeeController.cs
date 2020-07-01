using System.Threading.Tasks;
using Application;
using Domain;
using EmployeeApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeApp.Controllers
{
    /// <summary>
    /// Employee Controller class for processing API requests
    /// </summary>
    [Route("api/employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        /// <summary>
        /// Contructor initialization
        /// </summary>
        /// <param name="employeeService"></param>
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        /// <summary>
        /// Get Employee Details
        /// </summary>
        /// <returns></returns>
        // GET: api/employee
        [EnableCors("AllowOrigin")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var employeesList = await _employeeService.GetEmployees();
            return Ok(employeesList);
        }

        /// <summary>
        /// Get Employee By ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/employee/5
        [EnableCors("AllowOrigin")]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(string id)
        {
            var employee = await _employeeService.GetEmployeeById(id);
            if(employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        /// <summary>
        /// Add an employee
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        // POST: api/employee
        [Authorize]
        [EnableCors("AllowOrigin")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Post([FromBody] EmployeeViewModel employee)
        {
            var employeeData = ConvertEmployeeModelToEmployee(employee);
            var createdEmployee = await _employeeService.AddEmployee(employeeData);

            return CreatedAtAction(nameof(Get), new { employeeId = createdEmployee.Id.ToString() }, createdEmployee);
        }


        /// <summary>
        /// Update employee details
        /// </summary>
        /// <param name="id"></param>
        /// <param name="employeeViewModel"></param>
        // PUT: api/employee/5
        [Authorize]
        [EnableCors("AllowOrigin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] EmployeeViewModel employeeViewModel)
        {
            if(id != employeeViewModel.Id)
            {
                return BadRequest();
            }
            var employee = ConvertEmployeeModelToEmployee(employeeViewModel);
            var result = await _employeeService.UpdateEmployeeDetails(employee, id);

            // Not Found and Upadted 
            if(!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        /// <summary>
        /// Delete Employee based on ID
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        // DELETE: api/employee/5
        [EnableCors("AllowOrigin")]
        [HttpDelete("{employeeId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
        public async Task<IActionResult> Delete(string employeeId)
        {
            var result = await _employeeService.RemoveEmployee(employeeId);
            if(result)
            {
                return NoContent();
            }
            else
            {
                // 404 error
                return NotFound();
            }
        }

        /// <summary>
        /// Convert employee view model to employee model class
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        private Employee ConvertEmployeeModelToEmployee(EmployeeViewModel employee)
        {
            var employeeData = new Employee
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Department = employee.Department,
                Address = employee.Address,
                City = employee.City,
                Country = employee.Country,
                Designation = employee.Designation,
                PhoneNumber = employee.PhoneNumber
            };

            return employeeData;
        }
    }
}
