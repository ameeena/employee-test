using Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application
{
    public interface IEmployeeService
    {

        public Task<Employee> AddEmployee(Employee employee);

        public Task<bool> RemoveEmployee(string employeeId);

        public Task<List<Employee>> GetEmployees();

        public Task<Employee> GetEmployeeById(string employeeId);

    }
}
