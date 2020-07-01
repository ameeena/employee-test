using Application;
using Domain;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public class EmployeeContext
    {
        private readonly IMongoDatabase _database = null;
        private readonly IDbConfig _dbConfig;

        public EmployeeContext(IDbConfig dbConfig)
        {
            _dbConfig = dbConfig;
            var mongoClient = new MongoClient(_dbConfig.DbConnectionString);
            if(mongoClient != null)
            {
                _database= mongoClient.GetDatabase(_dbConfig.DbName);
            }
        }

        public IMongoCollection<Employee> Employees
        {
            get
            {
                return _database.GetCollection<Employee>(_dbConfig.EmployeeCollectionName);
            }
        }


    }
}
