using System.Threading.Tasks;
using Application;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="employeeService"></param>
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        // GET: api/Employee
        [EnableCors("AllowOrigin")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var employeesList = await _employeeService.GetEmployees();
            return Ok(employeesList);
        }

        // GET: api/Employee/5
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

        // POST: api/Employee
        [Authorize]
        [EnableCors("AllowOrigin")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Post([FromBody] Employee employee)
        {
            var createdEmployee = await _employeeService.AddEmployee(employee);

            return CreatedAtAction(nameof(Get), new { employeeId = createdEmployee.Id }, createdEmployee);
        }

        // PUT: api/Employee/5
        [EnableCors("AllowOrigin")]
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
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
    }
}
