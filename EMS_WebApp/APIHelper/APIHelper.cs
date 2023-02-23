using EMS.Core.Interfaces;
using EMS.Data.Enums;
using EMS.DTO.Common;
using EMS.DTO.EmployeeData;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;
using Microsoft.Extensions.Configuration;

namespace EMS.Razor.ApiHelper
{
    public class ApiHelper
    {
        private string _baseUri;

        public ApiHelper(string baseUri)
        {
            _baseUri = baseUri;
        }
        public List<DepartmentsEnum> GetDepartments()
        {
            List<DepartmentsEnum> departments = Enum.GetValues(typeof(DepartmentsEnum))
                            .Cast<DepartmentsEnum>()
                            .ToList();

            return departments;
        }
        public List<EmployeeDto> GetAllEmployees(string keyword)
        {

            List<EmployeeDto> employees = new List<EmployeeDto>();
            using (var client = new HttpClient())
            {
                HttpRequestMessage request = new HttpRequestMessage();
                string uri = !string.IsNullOrEmpty(keyword) ? $"{_baseUri}{ApiDirectory.GetAllEmployees}?keyword={keyword}" : $"{_baseUri}{ApiDirectory.GetAllEmployees}";

                request.RequestUri = new Uri(uri);
                request.Method = HttpMethod.Get;
                string result = client.Send(request).Content.ReadAsStringAsync().Result;
                var responseString = JsonConvert.DeserializeObject<ResponseDTO>(result);

                return responseString == null || responseString.Data == null
                    ? employees
                    : JsonConvert.DeserializeObject<List<EmployeeDto>>(responseString.Data.ToString().Trim().TrimStart('{').TrimEnd('}'));
            }

        }
        public  EmployeeDto GetEmployeeById(int employeeId)
        {

            using (var client = new HttpClient())
            {
                HttpRequestMessage request = new HttpRequestMessage();

                request.RequestUri = new Uri($"{_baseUri}{ApiDirectory.GetEmployeeById}?employeeId={employeeId}");
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
                // send the PUT request and get the response
                var response = await client.DeleteAsync($"{_baseUri}{ApiDirectory.RemoveEmployee}?employeeId={employeeId}");
                ResponseDTO responseDTO = JsonConvert.DeserializeObject<ResponseDTO>(response.Content.ReadAsStringAsync().Result);
                
                return responseDTO ?? new ResponseDTO();
            }

        }

        public async Task<ResponseDTO> UpdateEmployee(EmployeeDto? employeeDto)
        {
            if(employeeDto != null)
            {
                using (var client = new HttpClient())
                {
                    // serialize the employeeDto object to JSON
                    var jsonContent = new StringContent(JsonConvert.SerializeObject(employeeDto), Encoding.UTF8, "application/json");

                    // send the PUT request and get the response
                    var response = await client.PutAsync($"{_baseUri}{ApiDirectory.UpdateEmployee}", jsonContent);

                    ResponseDTO responseDTO = JsonConvert.DeserializeObject<ResponseDTO>(response.Content.ReadAsStringAsync().Result);
                    return responseDTO ?? new ResponseDTO();
                
                }
            }
            return new ResponseDTO();


        }

        public async Task<ResponseDTO> CreateEmployee(EmployeeDto employeeDto)
        {

            using (var client = new HttpClient())
            {
                ResponseDTO responseDTO = new ResponseDTO();
                // serialize the employeeDto object to JSON
                var jsonContent = new StringContent(JsonConvert.SerializeObject(employeeDto), Encoding.UTF8, "application/json");

                // send the PUT request and get the response
                var response = await client.PostAsync($"{_baseUri}{ApiDirectory.CreateEmployee}", jsonContent);

                responseDTO = JsonConvert.DeserializeObject<ResponseDTO>(response.Content.ReadAsStringAsync().Result);
                return responseDTO ?? new ResponseDTO();

            }

        }

    }
}
