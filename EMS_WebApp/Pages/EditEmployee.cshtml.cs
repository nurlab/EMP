using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EMS.Razor.APIHelper;
using EMS.DTO.EmployeeData;
using EMS.DTO.Common;
using Newtonsoft.Json;
using EMS.Core.Interfaces;
using EMS.Data.DbModels.EmployeeSchema;

namespace EMS.Razor.Pages
{
    public class EditEmployeeModel : PageModel
    {

        //public EditEmployeeModel()
        //{
        //    OnGetAsync(1);
        //}
        [BindProperty]
        public EmployeeDto employee { get; set; }

        [BindProperty]
        public string datrOfBirth { get; set; }
        public string message { get; set; }
        public List<Data.Enums.DepartmentsEnum> departments { get; set; }
        public int id;
        public void OnGet(int id)
        {
            EMS.Razor.APIHelper.APIHelper aPIHelper = new EMS.Razor.APIHelper.APIHelper();

            var res = aPIHelper.GetEmployeeById(id);

            departments = aPIHelper.GetDepartments();
            this.employee = res;
        }
        public async Task<IActionResult> OnPostAsync()
        {
            EMS.Razor.APIHelper.APIHelper aPIHelper = new EMS.Razor.APIHelper.APIHelper();

            ResponseDTO responseDTO = await aPIHelper.UpdateEmployee(employee);

            JsonConvert.DeserializeObject<EmployeeDto>(responseDTO.Data.ToString());

            if(responseDTO.IsPassed) return RedirectToPage("/Index"); // Redirect to the index page

            responseDTO.Message = message;

            return null;
        }

    }
}
