using Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application
{
    // Interface declaration for Employee Service
    public interface IEmployeeService
    {

        public Task<Employee> AddEmployee(Employee employee);

        public Task<bool> RemoveEmployee(string employeeId);

        public Task<List<Employee>> GetEmployees();

        public Task<Employee> GetEmployeeById(string employeeId);

        public Task<bool> UpdateEmployeeDetails(Employee employee, string employeeId);

    }
}
