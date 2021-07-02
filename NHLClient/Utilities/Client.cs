using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace NHLClient.Utilities
{
    public class Client
    {
        private readonly HttpClient _httpClient;

        public Client(string baseAddress)
        {
            _httpClient = new HttpClient();

            if (string.IsNullOrWhiteSpace(baseAddress) == false)
            {
                _httpClient.BaseAddress = new Uri(baseAddress);
            }
        }

        #region HttpMethods
        public async Task<HttpResponseMessage> GetAsync(string urlFragment, string urlParameters, string bearerToken)
        {
            AddBearerToken(bearerToken);

            string endpoint = GetEndpoint(urlFragment, urlParameters);

            HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync(endpoint);

            return httpResponseMessage;
        }
        #endregion

        #region Helpers
        private string GetEndpoint(string url, string urlParameters)
        {
            if (_httpClient.BaseAddress != null && string.IsNullOrWhiteSpace(_httpClient.BaseAddress.AbsoluteUri) == false)
            {
                if (_httpClient.BaseAddress.AbsoluteUri.EndsWith("/") == true && url.StartsWith("/") == true)
                {
                    url = url.Substring(1);
                }
                else if (_httpClient.BaseAddress.AbsoluteUri.EndsWith("/") == false && url.StartsWith("/") == false)
                {
                    url = $"/{url}";
                }
            }

            return $"{url}{urlParameters}";
        }
        private void AddBearerToken(string token)
        {
            _httpClient.DefaultRequestHeaders.Clear();

            if (string.IsNullOrWhiteSpace(token) == false)
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }
        #endregion
    }
}
