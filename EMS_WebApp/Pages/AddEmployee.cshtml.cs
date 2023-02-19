using EMS.Data.DbModels.EmployeeSchema;
using EMS.DTO.EmployeeData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using EMS.Razor.APIHelper;
using EMS.DTO.Common;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace EMS.Razor.Pages
{
    public class AddEmployeeModel : PageModel
    {
        public int id;
        public string message { get; set; }

        public EmployeeDto employee = new EmployeeDto();
        public List<Data.Enums.DepartmentsEnum> departments { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Name { get; set; }


        [BindProperty(SupportsGet = true)]
        public string Email { get; set; }


        [BindProperty(SupportsGet = true)]
        public string Department { get; set; }

        [BindProperty, DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        public void OnGet()
        {
            EMS.Razor.APIHelper.APIHelper aPIHelper = new EMS.Razor.APIHelper.APIHelper();

            departments = aPIHelper.GetDepartments();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            EMS.Razor.APIHelper.APIHelper aPIHelper = new EMS.Razor.APIHelper.APIHelper();

            this.employee.Name = this.Name;
            this.employee.Email = this.Email;
            this.employee.DateOfBirth = this.DateOfBirth;
            this.employee.Department = this.Department;

            ResponseDTO responseDTO = await aPIHelper.CreateEmployee(employee);

            JsonConvert.DeserializeObject<EmployeeDto>(responseDTO.Data.ToString());

            if (responseDTO.IsPassed) return RedirectToPage("/Index"); // Redirect to the index page

            responseDTO.Message = message;

            return null;
        }
    }
}


//namespace MyProject.Pages
//{
//    public class MyPageModel : PageModel
//    {
//        private readonly IMyService _myService;

//        public MyPageModel(IMyService myService)
//        {
//            _myService = myService;
//        }

//        [BindProperty]
//        public EmployeeModel Employee { get; set; }

//        public IActionResult OnGet()
//        {
//            // populate the departments dropdown
//            ViewData["Departments"] = new List<SelectListItem>
//            {
//                new SelectListItem("Department A", "A"),
//                new SelectListItem("Department B", "B"),
//                new SelectListItem("Department C", "C")
//            };
//            return Page();
//        }

       
//    }
//}