using Azure;
using EMS.Controllers;
using EMS.Core.Interfaces;
using EMS.Data.DbModels.EmployeeSchema;
using EMS.DTO.EmployeeData;
using EMS.Services.Employee;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EMS.WebApi.Controllers
{

    public class EmployeesController : BaseController
        {
            private readonly IEmployeeService _employeeService;

            public EmployeesController(
                  IEmployeeService employeeService,
                  IResponseDTO response,
                  IHttpContextAccessor httpContextAccessor) : base(response, httpContextAccessor)
            {
            _employeeService = employeeService;
            }


            [AllowAnonymous]
            [HttpGet]
            public IResponseDTO GetAllEmployees(string? keyword)
            {
                _response = _employeeService.GetAllEmployees(keyword);

                return _response;
            }

            [AllowAnonymous]
            [HttpGet]
            public IResponseDTO GetEmployeeById(int employeeId)
            {

                _response = _employeeService.GetEmployeeById(employeeId);
                return _response;
            }

        [HttpPost]
        public async Task<IResponseDTO> CreateEmployee([FromBody] EmployeeDto employeeDto)
        {
            _response = await _employeeService.CreateEmployee(employeeDto);
            return _response;
        }

        [AllowAnonymous]
        [HttpPut]
        public async Task<IResponseDTO> UpdateEmployee([FromBody] EmployeeDto employeeDto)
        {
            _response = await _employeeService.UpdateEmployee(employeeDto);
            return _response;
        }

        [AllowAnonymous]
        [HttpDelete]
        public async Task<IResponseDTO> RemoveEmployee(int employeeId)
        {
            _response = await _employeeService.RemoveEmployee(employeeId);
            return _response;
        }

    }

}
