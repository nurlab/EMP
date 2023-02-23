using EMS.Data.DbModels.EmployeeSchema;
using EMS.DTO.EmployeeData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using EMS.Razor.ApiHelper;
using EMS.DTO.Common;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Configuration;

namespace EMS.Razor.Pages
{
    public class AddEmployeeModel : PageModel
    {
//        IConfiguration configuration = new ConfigurationBuilder()
        public string? message { get; set; }

        public readonly EmployeeDto employee = new();
        public List<Data.Enums.DepartmentsEnum> departments { get; set; } = new List<Data.Enums.DepartmentsEnum>();

        [BindProperty(SupportsGet = true)]
        public string? Name { get; set; }


        [BindProperty(SupportsGet = true)]
        public string? Email { get; set; }


        [BindProperty(SupportsGet = true)]
        public string? Department { get; set; }

        [BindProperty, DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        private readonly string baseUri;

        public AddEmployeeModel(IConfiguration configuration)
        {
            baseUri = configuration.GetSection("BaseUrl").Value;
        }


        public void OnGet()
        {
            EMS.Razor.ApiHelper.ApiHelper _aPIHelper = new EMS.Razor.ApiHelper.ApiHelper(baseUri);

            departments = _aPIHelper.GetDepartments();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            EMS.Razor.ApiHelper.ApiHelper _aPIHelper = new EMS.Razor.ApiHelper.ApiHelper(baseUri);

            this.employee.Name = this.Name ?? "";
            this.employee.Email = this.Email ?? "";
            this.employee.DateOfBirth = this.DateOfBirth;
            this.employee.Department = this.Department ?? "";

            ResponseDTO responseDTO = await _aPIHelper.CreateEmployee(employee);

            JsonConvert.DeserializeObject<EmployeeDto>(responseDTO.Data.ToString());

            if (responseDTO.IsPassed) return RedirectToPage("/Index"); // Redirect to the index page

            responseDTO.Message = message == null ? "" : message;

            return RedirectToPage("/AddEmployee"); 
        }
    }
}