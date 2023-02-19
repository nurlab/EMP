using System;
using System.Collections.Generic;
using AutoMapper;
using EMS.Data.BaseModeling;
using EMS.Data.DbModels.EmployeeSchema;
using EMS.DTO.Common;
using EMS.DTO.EmployeeData;

namespace EMS.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<EMS.Data.DbModels.EmployeeSchema.Employee, EmployeeDto>().ReverseMap();
            CreateMap<EmployeeDto,EMS.Data.DbModels.EmployeeSchema.Employee>().ReverseMap();
        }


    }


}
