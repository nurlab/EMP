using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using EMS.Data.DbModels.EmployeeSchema;

namespace EMS.Data.DataContext
{
    public class DataSeedInitializations
    {
        private static AppDbContext _appDbContext;
        private static IServiceProvider _serviceProvider;

        public static void Seed(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _appDbContext.Database.EnsureCreated();
  


            SeedSampleEmployees();
 
        }

        private static void SeedSampleEmployees()
        {
            List<Employee> allEmployee = _appDbContext.Employees.ToList();

            //if (allEmployee == null || allEmployee.Count() == 0)
            //{
            //    _appDbContext.Employees.Add(new Employee
            //    { 
            //        Id = 1, Name = "John Doe", 
            //        Email = "john.doe@example.com", 
            //        DateOfBirth = new DateTime(1990, 1, 1), 
            //        Department = "Sales", IsDeleted = false, 
            //        IsActive = true, 
            //        CreatedOn = DateTime.Now 
            //    });
            //    _appDbContext.SaveChanges();

            //    _appDbContext.Employees.Add(new Employee
            //    { Id = 2, Name = "Jane Smith", 
            //        Email = "jane.smith@example.com", 
            //        DateOfBirth = new DateTime(1995, 5, 5), 
            //        Department = "Marketing", IsDeleted = false, 
            //        IsActive = true, 
            //        CreatedOn = DateTime.Now  });
            //    _appDbContext.SaveChanges();

            //    _appDbContext.Employees.Add(new Employee
            //    {   Id = 3, 
            //        Name = "Bob Johnson", 
            //        Email = "bob.johnson@example.com", 
            //        DateOfBirth = new DateTime(1985, 12, 31), 
            //        Department = "Finance", 
            //        IsDeleted = false, 
            //        IsActive = true, 
            //        CreatedOn = DateTime.Now  
            //    });

            //    _appDbContext.SaveChanges();

            //}

        }
    }
}
