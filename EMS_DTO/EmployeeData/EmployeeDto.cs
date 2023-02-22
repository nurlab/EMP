using EMS.DTO.Common;

namespace EMS.DTO.EmployeeData
{
    public class EmployeeDto : DynamicLookupDto
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }   
        public string Department { get; set; } = string.Empty;

    }
}
