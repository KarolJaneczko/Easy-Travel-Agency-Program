using System.Net;

namespace Travel_Agency_API.Models {
    public class ApiResponse {
        public HttpStatusCode StatusCode {  get; set; }
        public string? ErrorMessage { get; set; }
        public string? InnerMessage { get; set; }
        public ApiResponse(HttpStatusCode statusCode, string? errorMessage = null, string? innerMessage = null) {
            StatusCode = statusCode;
            ErrorMessage = errorMessage;
            InnerMessage = innerMessage;
        }
    }
}
