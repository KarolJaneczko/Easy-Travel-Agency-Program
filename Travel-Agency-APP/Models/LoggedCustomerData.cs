using System;

namespace Travel_Agency_APP.Models {
    public class LoggedCustomerData {
        public string UserLogin { get; set; }
        public string CustomerName { get; set; }
        public string CustomerSurname { get; set; }
        public DateTime CustomerBirthdate { get; set; }
        public string CustomerCountry { get; set; }
    }
}
