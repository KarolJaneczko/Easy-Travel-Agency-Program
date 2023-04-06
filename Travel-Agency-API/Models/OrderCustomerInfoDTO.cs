namespace Travel_Agency_API.Models {
    public class OrderCustomerInfoDTO {
        public int OrdersId { get; set; }
        public int CustomerId { get; set; }
        public string OrdersCountry { get; set; } = null!;
        public string OrdersCity { get; set; } = null!;
        public int OrdersLengthDays { get; set; }
        public string? OrdersDescription { get; set; }
        public string CustomerName { get; set; } = null!;
        public string CustomerSurname { get; set; } = null!;
        public DateTime CustomerBirthdate { get; set; }
        public string CustomerCountry { get; set; } = null!;
    }
}
