using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using Travel_Agency_APP.Helpers;
using Travel_Agency_APP.Models;
using Travel_Agency_APP.Utils;

namespace Travel_Agency_APP.Pages {
    public sealed partial class MainPage : Page {
        public static LoggedCustomerData CurrentCustomerData { get; set; }
        public static List<OrderCustomerInfo> ResultList { get; set; }
        public static ObservableCollection<Order> _Orders = new ObservableCollection<Order>();
        public static Order clickedOrder { get; set; }

        public ObservableCollection<Order> Orders {
            get { return _Orders; }
        }
        public MainPage() {
            InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e) {
            FillUserData(CurrentCustomerData);
            FillOrderList();
        }

        private void FillUserData(LoggedCustomerData customerInfo) {
            CurrentUserLogin.Text = customerInfo.UserLogin;
            CurrentUserName.Text = customerInfo.CustomerName;
            CurrentUserSurname.Text = customerInfo.CustomerSurname;
            CurrentUserBirthdate.Text = customerInfo.CustomerBirthdate.Date.ToString();
            CurrentUserCountry.Text = customerInfo.CustomerCountry;
        }

        public void FillOrderList() {
            _Orders.Clear();
            foreach (var order in ResultList) {
                _Orders.Add(new Order(order));
            }
        }

        private void LogoutButtonClick(object sender, RoutedEventArgs e) {
            AppProperties.SetCurrentLogin(string.Empty);
            Frame.Navigate(typeof(LoginPage));
        }

        private void AddButtonClick(object sender, RoutedEventArgs e) {
            AddUpdatePage.Insert = true;
            Frame.Navigate(typeof(AddUpdatePage));
        }

        private void UpdateButtonClick(object sender, RoutedEventArgs e) {
            if (clickedOrder == null) {
                ErrorMessage.Text = "Please check one of the shown orders!";
            } else {
                AddUpdatePage.Insert = false;
                AddUpdatePage.CurrentOrder = clickedOrder;
                Frame.Navigate(typeof(AddUpdatePage));
            }
        }

        private async void RemoveButtonClick(object sender, RoutedEventArgs e) {
            var orderID = clickedOrder.Id;
            var args = $"?orderID={orderID}";
            var result = await BaseClient.SendApiRequest(HttpMethod.Delete, string.Join(string.Empty, ApiPaths.DeleteOrder, args));
            _Orders.Remove(_Orders.First(x => x.Id == orderID));
        }

        public void ClickedOrder(object sender, ItemClickEventArgs e) {
            clickedOrder = _Orders.FirstOrDefault(x => x.OrderId.ToString() == e.ClickedItem.ToString());
            OrderCustomerName.Text = clickedOrder.CustomerName;
            OrderCustomerSurname.Text = clickedOrder.CustomerSurname;
            OrderCustomerBirthdate.Text = clickedOrder.CustomerBirthdate;
            OrderCustomerCountry.Text = clickedOrder.CustomerCountry;
        }
    }
    public class Order {
        public int OrderId { get; set; }
        public string OrderCountry { get; set; }
        public string OrderCity { get; set; }
        public int OrderDays { get; set; }
        public string OrderDescription { get; set; }

        public string CustomerName { get; set; }
        public string CustomerSurname { get; set; }
        public string CustomerBirthdate { get; set; }
        public string CustomerCountry { get; set; }

        public string Id { get { return OrderId.ToString(); } }
        public string Country { get { return $"Destination country: {OrderCountry}"; } }
        public string City { get { return $"Destination city:{OrderCity}"; } }
        public string Days { get { return $"Length of staying: {OrderDays} days"; } }
        public string Desc { get { return $"Info: {OrderDescription}"; } }

        public Order(OrderCustomerInfo order) {
            OrderId = order.OrdersId;
            OrderCountry = order.OrdersCountry;
            OrderCity = order.OrdersCity;
            OrderDays = order.OrdersLengthDays;
            OrderDescription = order.OrdersDescription;
            CustomerName = order.CustomerName;
            CustomerSurname = order.CustomerSurname;
            CustomerBirthdate = order.CustomerBirthdate.ToShortDateString();
            CustomerCountry = order.CustomerCountry;
        }
        public override string ToString() {
            return OrderId.ToString();
        }
    }
}
