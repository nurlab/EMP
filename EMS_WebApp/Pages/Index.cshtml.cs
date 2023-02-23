using EMS.Core.Interfaces;
using EMS.Data.DbModels.EmployeeSchema;
using EMS.DTO.Common;
using EMS.DTO.EmployeeData;
using EMS.Razor.ApiHelper;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace EMS_WebApp.Pages
{
    public class IndexModel : PageModel
    {
        public  List<EmployeeDto> employeeList { get; set; } = new List<EmployeeDto>();
        readonly ApiHelper _aPIHelper;
        [BindProperty]
        public string? keyword { get; set; }
        public string? message { get; set; }
        private readonly string baseUri;

        public IndexModel(IConfiguration configuration)
        {
             baseUri = configuration.GetSection("BaseUrl").Value;
            _aPIHelper = new ApiHelper(baseUri);
            GetAllEmployees(this.keyword ?? "");
        }
        public void Onget(string keyword)
        {
           this.keyword= keyword;
            GetAllEmployees(keyword);
        }
        public async Task<IActionResult> OnPostAsync(int? id = null, string? handler = null)
        {
            if (handler == "Delete")
            {

                ResponseDTO responseDTO = await _aPIHelper.RemoveEmployee(id);

                if (responseDTO != null && !responseDTO.IsPassed) message = responseDTO.Message; // Redirect to the index page

                
            }
            return RedirectToPage("/Index");
        }

        public void GetAllEmployees(string keyword)
        {
            this.employeeList = _aPIHelper.GetAllEmployees(this.keyword ?? "");

        }

    }
}

