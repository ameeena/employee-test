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
        public Task<Employee> AddEmployee(Employee employee)
        {
            _employees.InsertOne(employee);
            return Task.FromResult(employee);
        }

        public Task<Employee> GetEmployeeById(string employeeId)
        {
            var employee  = _employees.Find(employee => employee.Id == employeeId).FirstOrDefault();
            return Task.FromResult(employee);
        }

        public Task<List<Employee>> GetEmployees()
        {
            var employeesList = _employees.Find(employee => true).ToList();
            return Task.FromResult(employeesList);
        }

        public Task<bool> RemoveEmployee(string employeeId)
        {
            _employees.DeleteOne(employee => employee.Id == employeeId);
            return Task.FromResult(true);
        }
    }
}
