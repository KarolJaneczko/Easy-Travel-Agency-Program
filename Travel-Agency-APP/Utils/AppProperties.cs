namespace Travel_Agency_APP.Utils {
    public class AppProperties {
        public static string Login { get; set; }

        public static void SetCurrentLogin(string login) {
            Login = login;
        }
    }
}
