using System;
using System.Collections.Generic;
using System.Text;
using EMS.Repositories.Generics;

namespace EMS.Repositories.Employee
{
    public interface IEmployeeRepository : IGRepository<Data.DbModels.EmployeeSchema.Employee>
    {
    }
}
