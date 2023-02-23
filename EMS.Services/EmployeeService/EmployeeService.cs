using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using EMS.Core.Interfaces;
using EMS.Data.DataContext;
using EMS.Repositories.UOW;
using EMS.Services.Generics;
using System.Threading.Tasks;
using EMS.DTO.EmployeeData;
using EMS.Data.DbModels.EmployeeSchema;
using EMS.Repositories.Employee;

namespace EMS.Services.Employee
{
    public class EmployeeService : GService<EmployeeDto, EMS.Data.DbModels.EmployeeSchema.Employee, IEmployeeRepository>, IEmployeeService
    {

        private readonly IResponseDTO _response;
        private readonly IUnitOfWork<AppDbContext> _unitOfWork;
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IMapper mapper,
            IResponseDTO response,
            IEmployeeRepository employeeRepository,
            IUnitOfWork<AppDbContext> unitOfWork) : base(employeeRepository, mapper)
        {
            _employeeRepository = employeeRepository;
            _response = response;
            _unitOfWork = unitOfWork;
        }


        public IResponseDTO GetAllEmployees(string? keyword)
        {
            try
            {
                IList<EmployeeDto> employeesDto = new List<EmployeeDto>();

                var employeesList = _employeeRepository.GetAll(x=>x.IsDeleted != true).ToList();

                if(!string.IsNullOrEmpty(keyword)) employeesList = employeesList.Where(x=>x.Name.ToLower().Contains(keyword.ToLower())).ToList();


                employeesDto = _mapper.Map<List<EmployeeDto>>(employeesList);

                if (employeesDto == null)
                {
                    _response.Message = "No Employee Found";
                    _response.IsPassed = false;
                    return _response;
                }


                _response.Data = employeesDto;
                _response.Message = "Ok";
                _response.IsPassed = true;
            }
            catch (Exception ex)
            {
                _response.Data = new List<EmployeeDto>();
                _response.Message = "Error " + ex.Message;
                _response.IsPassed = false;
            }

            return _response;
        }

        public IResponseDTO GetEmployeeById(int employeeId)
        {
            try
            {
                var employee = _employeeRepository.GetFirstOrDefault(x => x.Id == employeeId);

                if (employee == null)
                {
                    _response.Message = "Employee not Found";
                    _response.IsPassed = false;
                    return _response;
                }

              

                var employeesDtoList = _mapper.Map<EmployeeDto>(employee);

         


                _response.Data = employeesDtoList;
                _response.Message = "Ok";
                _response.IsPassed = true;
            }
            catch (Exception ex)
            {
                _response.Data = new EmployeeDto();
                _response.Message = "Error " + ex.Message;
                _response.IsPassed = false;
            }

            return _response;
        }

        public async Task<IResponseDTO> CreateEmployee(EmployeeDto employeeDto)
        {
            try
            {

                var employee = _mapper.Map<EMS.Data.DbModels.EmployeeSchema.Employee>(employeeDto);


                // Add to the DB
                await _employeeRepository.AddAsync(employee);

                // Commit
                int save = await _unitOfWork.CommitAsync();
                if (save == 0)
                {
                    _response.Data = new EmployeeDto();
                    _response.IsPassed = false;
                    _response.Message = "Not saved";
                    return _response;
                }

                _response.Data = employee;
                _response.IsPassed = true;
                _response.Message = "Ok";
            }
            catch (Exception ex)
            {
                _response.Data = new EmployeeDto();
                _response.IsPassed = false;
                _response.Message = "Error " + ex.Message;
            }
            return _response;
        }

        public async Task<IResponseDTO> UpdateEmployee(EmployeeDto employeeDto)
        {
            try
            {
                var ExistingEmployee = await _employeeRepository.GetFirstAsync(x => x.Id == employeeDto.Id);
                if (employeeDto == null)
                {
                    _response.Data = new EmployeeDto();
                    _response.IsPassed = false;
                    _response.Message = "Invalid Employee";
                    return _response;
                }

                if (ExistingEmployee == null)
                {
                    _response.Data = new EmployeeDto();
                    _response.IsPassed = false;
                    _response.Message = "Employee Does not Exist";
                    return _response;
                }

                var employee = _mapper.Map<EMS.Data.DbModels.EmployeeSchema.Employee>(employeeDto);

                // Set relation variables with null to avoid unexpected EF errors
                employee.CreatedBy = employeeDto.CreatedBy;
                employee.UpdatedBy = null;

                _employeeRepository.Update(employee);

                // Commit
                int save = await _unitOfWork.CommitAsync();
                if (save == 0)
                {
                    _response.Data = new EmployeeDto();
                    _response.IsPassed = false;
                    _response.Message = "Not saved";
                    return _response;
                }
                _response.Data = employeeDto;
                _response.IsPassed = true;
                _response.Message = "Ok";

            }
            catch (Exception ex)
            {
                _response.Data = new EmployeeDto();
                _response.IsPassed = false;
                _response.Message = "Error " + ex.Message;
            }
            return _response;
        }

        public async Task<IResponseDTO> RemoveEmployee(int employeeId)
        {
            try
            {
                var employee = await _employeeRepository.GetFirstOrDefaultAsync(x => x.Id == employeeId);
                if (employee == null)
                {
                    _response.Data = new EmployeeDto();
                    _response.IsPassed = false;
                    _response.Message = "Invalid object id";
                    return _response;
                }

                employee.IsDeleted = true;
                employee.UpdatedBy = 0;
                employee.UpdatedOn = DateTime.Now;


                // Update on the Database
                _employeeRepository.Update(employee);

                // Commit
                int save = await _unitOfWork.CommitAsync();
                if (save == 0)
                {
                    _response.Data = new EmployeeDto();
                    _response.IsPassed = false;
                    _response.Message = "Not saved";
                    return _response;
                }

                _response.IsPassed = true;
                _response.Message = "Ok";
            }
            catch (Exception ex)
            {
                _response.Data = new EmployeeDto();
                _response.IsPassed = false;
                _response.Message = "Error " + ex.Message;
            }
            return _response;
        }

    }
}



