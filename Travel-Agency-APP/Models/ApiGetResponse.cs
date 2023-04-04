using System.Net;

namespace Travel_Agency_APP.Models {
    public class ApiGetResponse {
        public HttpStatusCode StatusCode { get; set; }
        public string ErrorMessage { get; set; }
        public string InnerMessage { get; set; }

    }
}