namespace Travel_Agency_API.Entities {
    public partial class Order {
        public int OrdersId { get; set; }
        public int CustomerId { get; set; }
        public string OrdersCountry { get; set; } = null!;
        public string OrdersCity { get; set; } = null!;
        public int OrdersLengthDays { get; set; }
        public string? OrdersDescription { get; set; }
        public virtual Customer Customer { get; set; } = null!;
    }
}