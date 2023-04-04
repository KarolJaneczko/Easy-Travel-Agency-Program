namespace Travel_Agency_API.Models {
    public partial class UserDTO {
        public string UserLogin { get; set; } = null!;
        public string UserPassword { get; set; } = null!;
        public bool UserIsAdmin { get; set; }
    }
}