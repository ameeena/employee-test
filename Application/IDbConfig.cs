﻿namespace Application
{
    public interface IDbConfig
    {
        public string EmployeeCollectionName { get; set; }

        public string DbConnectionString { get; set; }

        public string DbName { get; set; }
    }
}
