namespace Travel_Agency_API.Models {
    public partial class CustomerDTO {
        public string UserLogin { get; set; } = null!;
        public string CustomerName { get; set; } = null!;
        public string CustomerSurname { get; set; } = null!;
        public DateTime CustomerBirthdate { get; set; }
        public string CustomerCountry { get; set; } = null!;
    }
}