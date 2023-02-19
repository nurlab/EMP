using Autofac;
using EMS.Core.Interfaces;
using EMS.DTO.Common;
using EMS.DTO.EmployeeData;
using EMS.Repositories.Employee;
using EMS.Repositories.Generics;
using EMS.Repositories.UOW;
using EMS.Services.Common;
using EMS.Services.Employee;

namespace EMS.Services
{
    public class AutoFacConfiguration : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            // Register unit of work
            builder.RegisterGeneric(typeof(UnitOfWork<>)).As(typeof(IUnitOfWork<>)).InstancePerDependency();
            //// Register DTO
            builder.RegisterType<ResponseDTO>().As<IResponseDTO>().InstancePerLifetimeScope();
            builder.RegisterType<EmployeeService>().As<IEmployeeService>().InstancePerDependency();
            builder.RegisterType<EmployeeRepository>().As<IEmployeeRepository>().InstancePerDependency();



        }
    }
}
