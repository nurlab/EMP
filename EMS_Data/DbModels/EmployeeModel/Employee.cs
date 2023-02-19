using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using EMS.Data.BaseModeling;

namespace EMS.Data.DbModels.EmployeeSchema
{
    [Table("Employees", Schema="ems")]
    public  class Employee : BaseEntity
    {
            public string Name { get; set; }
            public string Email { get; set; }
            public DateTime DateOfBirth { get; set; }
            public string Department { get; set; }

    }
}
