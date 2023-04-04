namespace Travel_Agency_API.Entities {
    public partial class User {
        public int UserId { get; set; }
        public string UserLogin { get; set; } = null!;
        public string UserPassword { get; set; } = null!;
        public bool UserIsAdmin { get; set; }
        public virtual ICollection<Customer> Customers { get; } = new List<Customer>();
    }
}