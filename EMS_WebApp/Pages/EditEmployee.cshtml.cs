using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EMS.Razor.ApiHelper;
using EMS.DTO.EmployeeData;
using EMS.DTO.Common;
using Newtonsoft.Json;
using EMS.Core.Interfaces;
using EMS.Data.DbModels.EmployeeSchema;

namespace EMS.Razor.Pages
{
    public class EditEmployeeModel : PageModel
    {
        
        [BindProperty]
        public EmployeeDto employee { get; set; } = new EmployeeDto();

        [BindProperty]
        public string? datrOfBirth { get; set; }
        public string? message { get; set; }
        public List<Data.Enums.DepartmentsEnum> departments { get; set; } = new List<Data.Enums.DepartmentsEnum>();
        public int id;
        private readonly string baseUri;

        public EditEmployeeModel(IConfiguration configuration)
        {
            baseUri = configuration.GetSection("BaseUrl").Value;
        }
       
        public void OnGet(int id)
        {
            EMS.Razor.ApiHelper.ApiHelper _aPIHelper = new EMS.Razor.ApiHelper.ApiHelper(baseUri);

            var res = _aPIHelper.GetEmployeeById(id);

            departments = _aPIHelper.GetDepartments();
            this.employee = res;
        }
        public async Task<IActionResult> OnPostAsync()
        {
            EMS.Razor.ApiHelper.ApiHelper _aPIHelper = new EMS.Razor.ApiHelper.ApiHelper(baseUri);

            ResponseDTO responseDTO = await _aPIHelper.UpdateEmployee(employee);

            JsonConvert.DeserializeObject<EmployeeDto>(responseDTO.Data.ToString());

            if(responseDTO.IsPassed) return RedirectToPage("/Index"); // Redirect to the index page

            message = responseDTO.Message;

            return RedirectToPage("/EditEmployee"); // Redirect to the EditEmployee page
        }

    }
}
