using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Headers;
using System.Text.Json;
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
        private readonly ILogger<AuthService> _logger;
        private readonly ILocalStorageService _localStorage;
        private readonly NavigationManager _navigationManager;

        #endregion Fields

        #region Constructors

        public AuthService(IHttpClientFactory httpClientFactory,
            ILogger<AuthService> logger,
            IServiceProvider serviceProvider,
            ILocalStorageService localStorage,
            NavigationManager navigationManager)
        {
            _httpClient = httpClientFactory.CreateClient("API");
            _logger = logger;
            _localStorage = localStorage;
            _navigationManager = navigationManager;
        }

        #endregion Constructors

        #region Events

        public event Action<bool>? AuthStateChanged;

        #endregion Events

        #region Public Methods

        public async Task<AuthResult> LoginAsync(LoginModel model)
        {
            try
            {
                _logger.LogInformation("Starting login for email: {Email}", model.Email);

                var response = await _httpClient.PostAsJsonAsync("/api/auth/login", new
                {
                    email = model.Email,
                    password = model.Password
                });

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<AuthResult>();

                    if (result != null && result.Success && !string.IsNullOrEmpty(result.Token) && result.User != null)
                    {
                        // Store token and user data
                        await _localStorage.SetItemAsync(TokenKey, result.Token);
                        var userJson = JsonSerializer.Serialize(result.User);
                        await _localStorage.SetItemAsync(UserKey, userJson);

                        // Set authorization header
                        _httpClient.DefaultRequestHeaders.Authorization =
                            new AuthenticationHeaderValue("Bearer", result.Token);

                        // Notify auth state change via the provider
                        if (AuthStateChanged != null)
                        {
                            AuthStateChanged.Invoke(true);
                        }

                        _logger.LogInformation("Login successful for user: {UserName}", result.User.FullName);
                        return result;
                    }
                }

                var errorContent = await response.Content.ReadAsStringAsync();
                _logger.LogError("Login failed with status {StatusCode}: {Error}", response.StatusCode, errorContent);

                return new AuthResult
                {
                    Success = false,
                    Errors = { $"Login failed: {response.StatusCode}" }
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception during login");
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
                    if (result != null && result.Success && !string.IsNullOrEmpty(result.Token) && result.User != null)
                    {
                        // Store token
                        await _localStorage.SetItemAsync(TokenKey, result.Token);

                        // Store user data consistently using System.Text.Json
                        var userJson = JsonSerializer.Serialize(result.User);
                        await _localStorage.SetItemAsync(UserKey, userJson);

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
                _logger.LogError(ex, "Exception during registration");
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

                // Try to get cached user data
                var userJson = await _localStorage.GetItemAsync<string>(UserKey);
                if (!string.IsNullOrEmpty(userJson))
                {
                    try
                    {
                        var cachedUser = JsonSerializer.Deserialize<UserDto>(userJson);
                        if (cachedUser != null)
                            return cachedUser;
                    }
                    catch (JsonException ex)
                    {
                        _logger.LogWarning(ex, "Failed to deserialize cached user data");
                        // Continue to fetch from API
                    }
                }

                // Fetch from API if cache is empty or invalid
                var response = await _httpClient.GetAsync("api/auth/me");
                if (response.IsSuccessStatusCode)
                {
                    var user = await response.Content.ReadFromJsonAsync<UserDto>();
                    if (user != null)
                    {
                        // Cache the user data
                        var userJsonToStore = JsonSerializer.Serialize(user);
                        await _localStorage.SetItemAsync(UserKey, userJsonToStore);
                    }
                    return user;
                }

                await LogoutAsync();
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception getting current user");
                await LogoutAsync();
                return null;
            }
        }

        public async Task LogoutAsync()
        {
            try
            {
                await _localStorage.RemoveItemAsync(TokenKey);
                await _localStorage.RemoveItemAsync(UserKey);
                _httpClient.DefaultRequestHeaders.Authorization = null;

                // Notify auth state change
                AuthStateChanged?.Invoke(false);

                _navigationManager.NavigateTo("/", forceLoad: true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception during logout");
                // Still navigate even if there's an error
                _navigationManager.NavigateTo("/", forceLoad: true);
            }
        }

        public async Task<bool> IsAuthenticatedAsync()
        {
            try
            {
                var token = await _localStorage.GetItemAsync<string>(TokenKey);
                return !string.IsNullOrEmpty(token);
            }
            catch (InvalidOperationException)
            {
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception checking authentication status");
                return false;
            }
        }

        public async Task<string?> GetTokenAsync()
        {
            try
            {
                return await _localStorage.GetItemAsync<string>(TokenKey);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception getting token");
                return null;
            }
        }

        #endregion Public Methods
    }
}