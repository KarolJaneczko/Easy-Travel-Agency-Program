using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Travel_Agency_APP.Helpers;
using Travel_Agency_APP.Models;
using Travel_Agency_APP.Utils;

namespace Travel_Agency_APP.Pages {
    public sealed partial class AddUpdatePage : Page {
        public static bool Insert { get; set; }
        public static Order CurrentOrder { get; set; }
        public AddUpdatePage() {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {
            if (!Insert) {
                FillEntries(CurrentOrder);
            }
        }

        private async void AddUpdateClick(object sender, RoutedEventArgs e) {
            ClearError();
            var country = countryBox.Text;
            var city = cityBox.Text;
            var days = daysBox.Text;
            var info = infoBox.Text;

            try {
                if (Insert) {
                    var body = JsonConvert.SerializeObject(new NewOrder {
                        UserLogin = AppProperties.Login,
                        OrdersCountry = country,
                        OrdersCity = city,
                        OrdersLengthDays = int.Parse(days),
                        OrdersDescription = info
                    });
                    var result = await BaseClient.SendApiRequest(HttpMethod.Post, ApiPaths.CreateOrder, body);
                    var apiResponse = JsonConvert.DeserializeObject<ApiGetResponse>(result);
                    MainPage.ResultList = await GetOrderList();
                } else {
                    var body = JsonConvert.SerializeObject(new UpdateOrder {
                        OrdersId = MainPage.clickedOrder.OrderId,
                        OrdersCountry = country,
                        OrdersCity = city,
                        OrdersLengthDays = int.Parse(days),
                        OrdersDescription = info
                    });
                    var result = await BaseClient.SendApiRequest(HttpMethod.Put, ApiPaths.UpdateOrder, body);
                    var apiResponse = JsonConvert.DeserializeObject<ApiGetResponse>(result);
                    MainPage.ResultList = await GetOrderList();
                }
                Frame.Navigate(typeof(MainPage));
            } catch (Exception ex) {
                ShowError(ex.Message);
            }
        }
        private static async Task<List<OrderCustomerInfo>> GetOrderList() {
            var response = await BaseClient.SendApiRequest(HttpMethod.Get, string.Join(string.Empty, ApiPaths.GetOrders));
            var result = JsonConvert.DeserializeObject<List<OrderCustomerInfo>>(response);
            return result;
        }

        public void FillEntries(Order order) {
            countryBox.Text = order.OrderCountry;
            cityBox.Text = order.OrderCity;
            daysBox.Text = order.OrderDays.ToString();
            infoBox.Text = order.OrderDescription;
        }
        private void ShowError(string message) {
            ErrorMessage.Text = message;
        }

        private void ClearError() {
            ErrorMessage.Text = string.Empty;
        }
    }
}
