using EMS.Controllers;
using EMS.Core.Interfaces;
using EMS.DTO.EmployeeData;
using EMS.Services.Employee;
using EMS.WebApi.Controllers;
using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Xunit;

namespace EMS.Tests.Controllers
{
    public class EmployeesControllerTests
    {
        private readonly IEmployeeService _employeeService;
        private readonly EmployeesController _controller;
        private readonly IResponseDTO _response;
        private readonly HttpContext _httpContext;

        public EmployeesControllerTests()
        {
            _employeeService = A.Fake<IEmployeeService>();
            _response = A.Fake<IResponseDTO>();
            _httpContext = A.Fake<HttpContext>();
            _controller = new EmployeesController(_employeeService, _response, new HttpContextAccessor { HttpContext = _httpContext });
        }

        [Fact]
        public void GetAllEmployees_Returns_ResponseDTO()
        {
            // Arrange
            string keyword = "";

            A.CallTo(() => _employeeService.GetAllEmployees(keyword)).Returns(_response);

            // Act
            var result = _controller.GetAllEmployees(keyword);

            // Assert
            A.CallTo(() => _employeeService.GetAllEmployees(keyword)).MustHaveHappenedOnceExactly();
            Assert.Equal(result.IsPassed, _response.IsPassed);
            Assert.Equal(_response, result);
        }

        [Fact]
        public void GetEmployeeById_Returns_ResponseDTO()
        {
            // Arrange
            int employeeId = 1;

            A.CallTo(() => _employeeService.GetEmployeeById(employeeId)).Returns(_response);

            // Act
            var result = _controller.GetEmployeeById(employeeId);

            // Assert
            A.CallTo(() => _employeeService.GetEmployeeById(employeeId)).MustHaveHappenedOnceExactly();
            Assert.Equal(result.IsPassed, _response.IsPassed);
            Assert.Equal(_response, result);
        }

        [Fact]
        public async Task CreateEmployee_Returns_ResponseDTO()
        {
            // Arrange
            var employeeDto = A.Fake<EmployeeDto>();

            A.CallTo(() => _employeeService.CreateEmployee(employeeDto)).Returns(_response);

            // Act
            var result = await _controller.CreateEmployee(employeeDto);

            // Assert
            A.CallTo(() => _employeeService.CreateEmployee(employeeDto)).MustHaveHappenedOnceExactly();
            Assert.Equal(result.IsPassed, _response.IsPassed);
            Assert.Equal(_response, result);
        }

        [Fact]
        public async Task UpdateEmployee_Returns_ResponseDTO()
        {
            // Arrange
            var employeeDto = A.Fake<EmployeeDto>();

            A.CallTo(() => _employeeService.UpdateEmployee(employeeDto)).Returns(_response);

            // Act
            var result = await _controller.UpdateEmployee(employeeDto);

            // Assert
            A.CallTo(() => _employeeService.UpdateEmployee(employeeDto)).MustHaveHappenedOnceExactly();
            Assert.Equal(result.IsPassed, _response.IsPassed);
            Assert.Equal(_response, result);
        }

        [Fact]
        public async Task RemoveEmployee_Returns_ResponseDTO()
        {
            // Arrange
            int employeeId = 1;

            A.CallTo(() => _employeeService.RemoveEmployee(employeeId)).Returns(_response);

            // Act
            var result = await _controller.RemoveEmployee(employeeId);

            // Assert
            A.CallTo(() => _employeeService.RemoveEmployee(employeeId)).MustHaveHappenedOnceExactly();
            Assert.Equal(result.IsPassed, _response.IsPassed);
            Assert.Equal(_response, result);
        }
    }
}