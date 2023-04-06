using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
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
    public sealed partial class LoginPage : Page {
        public LoginPage() {
            InitializeComponent();
        }

        private async void LoginButtonClick(object sender, RoutedEventArgs e) {
            ClearError();
            var login = loginBox.Text;
            var password = passwordBox.Password.ToString();
            var args = $"?login={login}&password={password}";

            try {
                var result = await BaseClient.SendApiRequest(HttpMethod.Get, string.Join(string.Empty, ApiPaths.Login, args));
                var apiResponse = JsonConvert.DeserializeObject<ApiGetResponse>(result);

                if (apiResponse.StatusCode == HttpStatusCode.OK) {
                    AppProperties.SetCurrentLogin(login);
                    MainPage.CurrentCustomerData = await GetCurrentCustomerInfo(login);
                    MainPage.ResultList = await GetOrderList();
                    Frame.Navigate(typeof(MainPage));
                } else {
                    ShowError(apiResponse.ErrorMessage);
                }

            } catch (Exception ex) {
                ShowError(ex.Message);
            }
        }
        private static async Task<LoggedCustomerData> GetCurrentCustomerInfo(string login) {
            var response = await BaseClient.SendApiRequest(HttpMethod.Get, string.Join(string.Empty, ApiPaths.GetCustomer, $"?login={login}"));
            var result = JsonConvert.DeserializeObject<LoggedCustomerData>(response);
            return result;
        }

        private static async Task<List<OrderCustomerInfo>> GetOrderList() {
            var response = await BaseClient.SendApiRequest(HttpMethod.Get, string.Join(string.Empty, ApiPaths.GetOrders));
            var result = JsonConvert.DeserializeObject<List<OrderCustomerInfo>>(response);
            return result;
        }

        private void ShowError(string message) {
            ErrorMessage.Text = message;
        }

        private void ClearError() {
            ErrorMessage.Text = string.Empty;
        }
    }
}
