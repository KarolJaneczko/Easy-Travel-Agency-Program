using System;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Travel_Agency_APP.Helpers;

namespace Travel_Agency_APP.Utils {
    public static class BaseClient {
        private static readonly HttpClient httpClient;
        static BaseClient() {
            HttpClientHandler clientHandler = new HttpClientHandler { UseProxy = false };
            httpClient = new HttpClient(clientHandler) {
                BaseAddress = new Uri(ApiPaths.Address),
            };
        }

        public static async Task<string> SendApiRequest(HttpMethod method, string path, string body = "") {
            var request = new HttpRequestMessage {
                Method = method,
                RequestUri = new Uri(string.Join(string.Empty, ApiPaths.Address, path)),
                Content = new StringContent(body, Encoding.UTF8, "application/json"),
            };

            var response = await httpClient.SendAsync(request).ConfigureAwait(false);
            if (response.StatusCode == HttpStatusCode.OK)
                return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return null;
        }
    }
}
