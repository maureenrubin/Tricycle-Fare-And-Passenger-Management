using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Headers;
using TricycleFareAndPassengerManagement.Client.Models;
using TricycleFareAndPassengerManagement.Client.Services.Interfaces;

namespace TricycleFareAndPassengerManagement.Client.Services
{
    public class AuthService : IAuthService
    {
        #region Fields

        private const string TokenKey = "authToken";
        private const string UserKey = "currentUser";
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly NavigationManager _navigationManager;

        #endregion Fields

        #region Public Constructors

        public AuthService(HttpClient httpClient, ILocalStorageService localStorage, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            _navigationManager = navigationManager;
        }

        #endregion Public Constructors

        #region Events

        public event Action<bool>? AuthStateChanged;

        #endregion Events

        #region Public Methods

        public async Task<AuthResult> LoginAsync(LoginModel model)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/api/auth/login", new
                {
                    email = model.Email,
                    password = model.Password
                });

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<AuthResult>();
                    if (result != null && result.Success && !string.IsNullOrEmpty(result.Token))
                    {
                        await _localStorage.SetItemAsync(TokenKey, result.Token);
                        await _localStorage.SetItemAsync(UserKey, result.User);

                        _httpClient.DefaultRequestHeaders.Authorization =
                            new AuthenticationHeaderValue("Bearer", result.Token);

                        AuthStateChanged?.Invoke(true);
                        return result;
                    }
                }

                var errorContent = await response.Content.ReadAsStringAsync();
                return new AuthResult
                {
                    Success = false,
                    Errors = { $"Login failed: {response.StatusCode}" }
                };
            }
            catch (Exception ex)
            {
                return new AuthResult
                {
                    Success = false,
                    Errors = { $"An error occurred: {ex.Message}" }
                };
            }
        }

        public async Task<AuthResult> RegisterAsync(RegisterModel model)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/api/auth/register", new
                {
                    fullName = model.FullName,
                    email = model.Email,
                    password = model.Password
                });

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<AuthResult>();
                    if (result != null && result.Success && !string.IsNullOrEmpty(result.Token))
                    {
                        await _localStorage.SetItemAsync(TokenKey, result.Token);
                        await _localStorage.SetItemAsync(UserKey, result.User);

                        _httpClient.DefaultRequestHeaders.Authorization =
                            new AuthenticationHeaderValue("Bearer", result.Token);

                        AuthStateChanged?.Invoke(true);
                        return result;
                    }
                }

                var errorContent = await response.Content.ReadAsStringAsync();
                return new AuthResult
                {
                    Success = false,
                    Errors = { $"Registration failed: {response.StatusCode}" }
                };
            }
            catch (Exception ex)
            {
                return new AuthResult
                {
                    Success = false,
                    Errors = { $"An error occurred: {ex.Message}" }
                };
            }
        }

        public async Task<UserDto?> GetCurrentUserAsync()
        {
            try
            {
                if (!await IsAuthenticatedAsync())
                    return null;

                // Try to get user from local storage first
                var cachedUser = await _localStorage.GetItemAsync<UserDto>(UserKey);
                if (cachedUser != null)
                    return cachedUser;

                // If not in cache, get from API
                var response = await _httpClient.GetAsync("/api/auth/me");
                if (response.IsSuccessStatusCode)
                {
                    var user = await response.Content.ReadFromJsonAsync<UserDto>();
                    if (user != null)
                    {
                        await _localStorage.SetItemAsync(UserKey, user);
                    }
                    return user;
                }

                // If API call fails, user might not be authenticated
                await LogoutAsync();
                return null;
            }
            catch
            {
                await LogoutAsync();
                return null;
            }
        }

        public async Task LogoutAsync()
        {
            await _localStorage.RemoveItemAsync(TokenKey);
            await _localStorage.RemoveItemAsync(UserKey);
            _httpClient.DefaultRequestHeaders.Authorization = null;
            AuthStateChanged?.Invoke(false);
            _navigationManager.NavigateTo("/login");
        }

        public async Task<bool> IsAuthenticatedAsync()
        {
            try
            {
                var token = await _localStorage.GetItemAsStringAsync(TokenKey);
                return !string.IsNullOrEmpty(token);
            }
            catch (InvalidOperationException)
            {
                return false;
            }
        }

        public async Task<string?> GetTokenAsync()
        {
            return await _localStorage.GetItemAsStringAsync(TokenKey);
        }

        #endregion Public Methods
    }
}