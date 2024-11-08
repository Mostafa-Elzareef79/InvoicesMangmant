

using ItRootsTask_Business;
using ItRootsTask_Core.Enums;
using Microsoft.Extensions.Localization;

namespace ItRootsTask_Core
{
    public class Response<T>
    {
        private readonly IStringLocalizer<SharedResource> localizer;
        public bool succeeded { get; set; }
        public string message { get; set; }
        public List<string> errors { get; set; } = new List<string>();
        public T data { get; set; }
        public HttpStatuses statusCode { get; set; }
        public Response()
        {
        }
        public Response(IStringLocalizer<SharedResource> _localizer)
        {
            localizer = _localizer;
        }
        private Response(string Errormessage)
        {
            succeeded = false;
            statusCode = Errormessage == SharedResourceMessages.ErrorMsg || Errormessage == SharedResourceMessages.unExpectedError ? HttpStatuses.Status500InternalServerError : HttpStatuses.Status401Unauthorized;
            errors = new List<string> { Errormessage };
        }
        public Response(T _data, HttpStatuses _statusCode, string _message = null)
        {
            succeeded = true;
            statusCode = _statusCode;
            message = _message;
            data = _data;
        }
        public Response(HttpStatuses _statusCode, string errorMessage)
        {
            succeeded = false;
            statusCode = _statusCode;
            errors = new List<string> { errorMessage };
        }


        public static implicit operator Response<T>(string ErrorMessage)
        {
            return new Response<T>(ErrorMessage);
        }
    }
}
