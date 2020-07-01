using Application;
using Domain;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IMongoCollection<Employee> _employees;
        public EmployeeService(IDbConfig dbConfig)
        {
            var mongoClient = new MongoClient(dbConfig.DbConnectionString);
            var db = mongoClient.GetDatabase(dbConfig.DbName);

            _employees = db.GetCollection<Employee>(dbConfig.EmployeeCollectionName);
        }

        /// <summary>
        /// Add an employee to the DB
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public Task<Employee> AddEmployee(Employee employee)
        {
            _employees.InsertOne(employee);
            return Task.FromResult(employee);
        }

        /// <summary>
        /// Get Employee based on ID
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public Task<Employee> GetEmployeeById(string employeeId)
        {
            var employee  = _employees.Find(employee => employee.Id == employeeId).FirstOrDefault();
            return Task.FromResult(employee);
        }

        /// <summary>
        /// Get list of all the employees
        /// </summary>
        /// <returns></returns>
        public Task<List<Employee>> GetEmployees()
        {
            var employeesList = _employees.Find(employee => true).ToList();
            return Task.FromResult(employeesList);
        }

        /// <summary>
        /// Remove employee based on ID
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public Task<bool> RemoveEmployee(string employeeId)
        {
            var result = _employees.DeleteOne(employee => employee.Id == employeeId);
            return result.DeletedCount == 1 ? Task.FromResult(true) : Task.FromResult(false);

        }
    }
}
