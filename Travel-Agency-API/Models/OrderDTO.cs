namespace Travel_Agency_API.Models {
    public partial class OrderDTO {
        public string? UserLogin { get; set; }
        public string OrdersCountry { get; set; } = null!;
        public string OrdersCity { get; set; } = null!;
        public int OrdersLengthDays { get; set; }
        public string? OrdersDescription { get; set; }
    }
}
