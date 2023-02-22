using Microsoft.EntityFrameworkCore;
using System.Linq;
using EMS.Data.DbModels.EmployeeSchema;
using Microsoft.Extensions.Options;

namespace EMS.Data.DataContext
{
    public class AppDbContext : DbContext
    {
        private readonly DbContextOptions<AppDbContext> _options;
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            _options =  options;
        }
        
        public DbSet<Employee> Employees { get; set; }
    }
}
