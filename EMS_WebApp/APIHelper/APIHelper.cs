using EMS.Core.Interfaces;
using EMS.Data.Enums;
using EMS.DTO.Common;
using EMS.DTO.EmployeeData;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace EMS.Razor.APIHelper
{
    public class APIHelper
    {
        public List<DepartmentsEnum> GetDepartments()
        {
            List<DepartmentsEnum> departments = Enum.GetValues(typeof(DepartmentsEnum))
                            .Cast<DepartmentsEnum>()
                            .ToList();

            return departments;
        }
        public List<EmployeeDto> GetAllEmployees(string keyword)
        {

            using (var client = new HttpClient())
            {
                HttpRequestMessage request = new HttpRequestMessage();
                string uri;
                uri = !string.IsNullOrEmpty(keyword) ? $"https://localhost:7063/api/Employees/GetAllEmployees?keyword={keyword}" :  $"https://localhost:7063/api/Employees/GetAllEmployees";

                request.RequestUri = new Uri(uri);
                request.Method = HttpMethod.Get;
                var responseString = JsonConvert.DeserializeObject<ResponseDTO>(client.Send(request).Content.ReadAsStringAsync().Result);
                
                if(responseString.Data != null) return JsonConvert.DeserializeObject<List<EmployeeDto>>(responseString.Data.ToString().Trim().TrimStart('{').TrimEnd('}'));
                return null;
            }

        }
        public  EmployeeDto GetEmployeeById(int employeeId)
        {

            using (var client = new HttpClient())
            {
                HttpRequestMessage request = new HttpRequestMessage();
                request.RequestUri = new Uri($"https://localhost:7063/api/Employees/GetEmployeeById?employeeId={employeeId}");
                request.Method = HttpMethod.Get;
                var responseString = JsonConvert.DeserializeObject<ResponseDTO>(client.Send(request).Content.ReadAsStringAsync().Result);
                EmployeeDto _EmployeeDto = JsonConvert.DeserializeObject<EmployeeDto>(responseString.Data.ToString());
                return _EmployeeDto;
            }

        }       

        public async Task<ResponseDTO> RemoveEmployee(int? employeeId)
        {

            using (var client = new HttpClient())
            {
                ResponseDTO responseDTO = new ResponseDTO();

                // send the PUT request and get the response
                var response = await client.DeleteAsync($"https://localhost:7063/api/Employees/RemoveEmployee?employeeId={employeeId}");

                responseDTO = JsonConvert.DeserializeObject<ResponseDTO>(response.Content.ReadAsStringAsync().Result);
                return responseDTO;
            }

        }

        public async Task<ResponseDTO> UpdateEmployee(EmployeeDto employeeDto)
        {

            using (var client = new HttpClient())
            {
                ResponseDTO responseDTO = new ResponseDTO();
                // serialize the employeeDto object to JSON
                var jsonContent = new StringContent(JsonConvert.SerializeObject(employeeDto), Encoding.UTF8, "application/json");

                // send the PUT request and get the response
                var response = await client.PutAsync("https://localhost:7063/api/Employees/UpdateEmployee", jsonContent);

                //var response = await client.PutAsJsonAsync(request.RequestUri, new { employeeDto });

                responseDTO = JsonConvert.DeserializeObject<ResponseDTO>(response.Content.ReadAsStringAsync().Result);
                return responseDTO;
                
            }

        }

        public async Task<ResponseDTO> CreateEmployee(EmployeeDto employeeDto)
        {

            using (var client = new HttpClient())
            {
                ResponseDTO responseDTO = new ResponseDTO();
                // serialize the employeeDto object to JSON
                var jsonContent = new StringContent(JsonConvert.SerializeObject(employeeDto), Encoding.UTF8, "application/json");

                // send the PUT request and get the response
                var response = await client.PostAsync("https://localhost:7063/api/Employees/CreateEmployee", jsonContent);

                //var response = await client.PutAsJsonAsync(request.RequestUri, new { employeeDto });

                responseDTO = JsonConvert.DeserializeObject<ResponseDTO>(response.Content.ReadAsStringAsync().Result);
                return responseDTO;

            }

        }


        //Task<IResponseDTO> CreateEmployee(EmployeeDto employeeDto);
        //IResponseDTO GetAllEmployees();
        //IResponseDTO GetEmployeeById(int employeeId);
        //Task<IResponseDTO> RemoveEmployee(int employeeId);
        //Task<IResponseDTO> UpdateEmployee(EmployeeDto employeeDto);

    }
}
