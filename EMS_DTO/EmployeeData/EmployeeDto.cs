using EMS.DTO.Common;

namespace EMS.DTO.EmployeeData
{
    public class EmployeeDto : DynamicLookupDto
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Department { get; set; }

    }
}
