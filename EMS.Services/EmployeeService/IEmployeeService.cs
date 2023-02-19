using EMS.Core.Interfaces;
using EMS.DTO.EmployeeData;

namespace EMS.Services.Employee
{
    public interface IEmployeeService
    {
        Task<IResponseDTO> CreateEmployee(EmployeeDto employeeDto);
        IResponseDTO GetAllEmployees(string? keyword);
        IResponseDTO GetEmployeeById(int employeeId);
        Task<IResponseDTO> RemoveEmployee(int employeeId);
        Task<IResponseDTO> UpdateEmployee(EmployeeDto employeeDto);
    }
}