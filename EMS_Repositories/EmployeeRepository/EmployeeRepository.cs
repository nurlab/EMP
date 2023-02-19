using System;
using System.Collections.Generic;
using System.Text;
using EMS.Data.DataContext;
using EMS.Repositories.Generics;

namespace EMS.Repositories.Employee
{
    public class EmployeeRepository : GRepository<Data.DbModels.EmployeeSchema.Employee>, IEmployeeRepository
    {
        private readonly AppDbContext _appDbContext;
        public EmployeeRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }
    }
}
