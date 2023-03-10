using EMS.Core.Interfaces;

namespace EMS.DTO.Common
{
    public class ResponseDTO : IResponseDTO
    {
        public ResponseDTO()
        {
            IsPassed = false;
            Message = "";
        }
        public bool IsPassed { get; set; }

        public string Message { get; set; }

        public dynamic Data { get; set; } 
    }
}
