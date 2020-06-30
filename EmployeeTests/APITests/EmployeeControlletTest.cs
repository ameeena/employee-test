using Application;
using Domain;
using EmployeeApp.Controllers;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeTests.APITests
{
    [TestClass]
    public class EmployeeControlletTest
    {
        private readonly Mock<IEmployeeService> _mockEmployeeService;
        private readonly EmployeeController _employeeController;
        public EmployeeControlletTest()
        {
            _mockEmployeeService = new Mock<IEmployeeService>();
            _employeeController = new EmployeeController(_mockEmployeeService.Object);
        }

        [TestMethod]
        public void GetEmployeeReturnOkResult()
        {
            List<Employee> employees = new List<Employee>
            {
                new Employee
                {
                    Id = "01_Id1",
                    FirstName= "Mairaj",
                    LastName = "Ameena",
                    Department = "Dept1"
                },
                new Employee
                {
                    Id = "02_Id2",
                    FirstName= "Farheen",
                    LastName = "Khan",
                    Department = "Dept2"
                },
                new Employee
                {
                    Id = "03_Id3",
                    FirstName= "FirstName1",
                    LastName = "LastName2",
                    Department = "Dept3"
                }
            };

            //set-up
            _mockEmployeeService.Setup(repo => repo.GetEmployees()).Returns(Task.FromResult(employees));
            // Act
            var response = _employeeController.Get().Result;
            var contentResult = response as OkObjectResult;
            var employeeList = contentResult.Value as List<Employee>;


            Assert.IsNotNull(contentResult);
            Assert.IsInstanceOfType(contentResult.Value, typeof(List<Employee>));
            Assert.AreEqual(3, employeeList.Count);
        }

        [TestMethod]
        public void GetClientByIdReturnOkResult()
        {
            var employeeId = "01_Id1";
            var employee = new Employee
            {
                Id = "01_Id1",
                FirstName = "Mairaj",
                LastName = "Ameena",
                Department = "Dept1"
            };

            _mockEmployeeService.Setup(repo => repo.GetEmployeeById(employeeId)).Returns(Task.FromResult(employee));

            //Get

            var response = _employeeController.Get(employeeId).Result;
            var contentResult = response as OkObjectResult;
            var recivedEmployee = contentResult.Value as Employee;

            Assert.IsNotNull(recivedEmployee);
            Assert.IsInstanceOfType(contentResult.Value, typeof(Employee));
            Assert.AreEqual(employeeId, recivedEmployee.Id);
        }

        [TestMethod]
        public void AddEmployeeOkResult()
        {
            var expectedEmployeeId = "001_ID1";
            var newEmployee = new Employee
            {
                Id = "001_ID1",
                FirstName = "Mairaj",
                LastName = "Ameena",
                Department = "Dept1"
            };

            //Set up
            _mockEmployeeService.Setup(repo => repo.AddEmployee(newEmployee)).Returns(Task.FromResult(newEmployee));

            //Get

            var response = _employeeController.Post(newEmployee).Result;
            Assert.IsInstanceOfType(response, typeof(CreatedAtActionResult));

            var contentResult = response as CreatedAtActionResult;
            var addedEmployee = contentResult.Value as Employee;

            Assert.IsNotNull(addedEmployee);
            Assert.AreEqual(expectedEmployeeId, addedEmployee.Id);
        }

        [TestMethod]
        public void DeleteEmployeeOkResult()
        {
            var employeeId = "001_ID1";

            _mockEmployeeService.Setup(repo => repo.RemoveEmployee(employeeId)).Returns(Task.FromResult(true));

            var response = _employeeController.Delete(employeeId).Result;

            Assert.IsInstanceOfType(response, typeof(NoContentResult));
        }
        // Negative test cases
    }
}
