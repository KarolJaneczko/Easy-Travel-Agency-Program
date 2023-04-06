namespace Travel_Agency_APP.Helpers {
    public class ApiPaths {
        public static readonly string Address = "https://localhost:7109/api";
        public static readonly string Login = "/User/Login";
        public static readonly string GetCustomer = "/Customer/GetCustomer";
        public static readonly string GetOrders = "/Order/ShowOrdersWithCustomerInfo";
        public static readonly string CreateOrder = "/Order/CreateOrder";
        public static readonly string UpdateOrder = "/Order/UpdateOrder";
        public static readonly string DeleteOrder = "/Order/DeleteOrder";
    }
}
