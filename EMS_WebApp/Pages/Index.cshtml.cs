using EMS.Core.Interfaces;
using EMS.Data.DbModels.EmployeeSchema;
using EMS.DTO.Common;
using EMS.DTO.EmployeeData;
using EMS.Razor.APIHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace EMS_WebApp.Pages
{
    public class IndexModel : PageModel
    {
        public  List<EmployeeDto> employeeList;
        APIHelper aPIHelper = new APIHelper();
        [BindProperty]
        public string keyword { get; set; }
        
        public IndexModel()
        {
            this.employeeList = null;
            GetAllEmployees(this.keyword);
        }
        public void Onget(string keyword)
        {
           this.keyword= keyword;
            GetAllEmployees(keyword);
        }
        public async Task<IActionResult> OnPostAsync(int? id = null, string handler = null)
        {
            if (handler == "Delete")
            {

                ResponseDTO responseDTO = await aPIHelper.RemoveEmployee(id);

                if (responseDTO.IsPassed) return RedirectToPage("/Index"); // Redirect to the index page

                return null;
            }
            return null;
        }

        public void GetAllEmployees(string keyword)
        {
            this.employeeList = null;
            this.employeeList = aPIHelper.GetAllEmployees(this.keyword);

        }

    }
}

