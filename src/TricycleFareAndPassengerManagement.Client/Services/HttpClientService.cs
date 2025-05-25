using Blazored.LocalStorage;
using System.Net.Http.Headers;

namespace TricycleFareAndPassengerManagement.Client.Services
{
    public class HttpClientService
    {
        #region Fields

        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;

        #endregion Fields

        #region Public Constructors

        public HttpClientService(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<HttpClient> GetAuthenticatedClientAsync()
        {
            var token = await _localStorage.GetItemAsStringAsync("authToken");
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }
            return _httpClient;
        }

        #endregion Public Methods
    }
}