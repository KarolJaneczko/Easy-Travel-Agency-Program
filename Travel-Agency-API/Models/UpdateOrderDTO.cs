namespace Travel_Agency_API.Models {
    public class UpdateOrderDTO {
        public int OrdersId { get; set; }
        public string OrdersCountry { get; set; } = null!;
        public string OrdersCity { get; set; } = null!;
        public int OrdersLengthDays { get; set; }
        public string? OrdersDescription { get; set; }
    }
}
