namespace Travel_Agency_API.Entities {
    public partial class Customer {
        public int CustomerId { get; set; }
        public int UserId { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerSurname { get; set; }
        public DateTime? CustomerBirthdate { get; set; }
        public string? CustomerCountry { get; set; }
        public virtual ICollection<Order> Orders { get; } = new List<Order>();
        public virtual User User { get; set; } = null!;
    }
}