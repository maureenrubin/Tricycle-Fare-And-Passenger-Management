using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Security.Claims;
using TricycleFareAndPassengerManagement.Client.Models;
using TricycleFareAndPassengerManagement.Client.Services.Interfaces;

namespace TricycleFareAndPassengerManagement.Client.Security
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        #region Fields

        private readonly ILocalStorageService _localStorage;
        private readonly IAuthService _authService;
        private readonly IJSRuntime _jsRuntime;
        private AuthenticationState _cachedAuthState;

        #endregion Fields

        #region Public Constructors

        public CustomAuthStateProvider(ILocalStorageService localStorage, IAuthService authService, IJSRuntime jsRuntime)
        {
            _localStorage = localStorage;
            _authService = authService;
            _jsRuntime = jsRuntime;
            _authService.AuthStateChanged += OnAuthStateChanged;
            // Initialize with anonymous state for prerendering
            _cachedAuthState = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        #endregion Public Constructors

        #region Public Methods

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            // During prerendering, return anonymous state immediately
            if (!OperatingSystem.IsBrowser())
            {
                return _cachedAuthState;
            }

            try
            {
                // DEBUG: Log the authentication check
                await _jsRuntime.InvokeVoidAsync("console.log", "CustomAuthStateProvider: Getting authentication state");

                var token = await _localStorage.GetItemAsStringAsync("authToken");
                var user = await _localStorage.GetItemAsync<UserDto>("currentUser");

                // DEBUG: Log what we found
                await _jsRuntime.InvokeVoidAsync("console.log", $"CustomAuthStateProvider: Token exists: {!string.IsNullOrEmpty(token)}, User exists: {user != null}");

                if (string.IsNullOrEmpty(token) || user == null)
                {
                    await _jsRuntime.InvokeVoidAsync("console.log", "CustomAuthStateProvider: No valid auth data, returning anonymous state");
                    _cachedAuthState = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
                    return _cachedAuthState;
                }

                // DEBUG: Log successful authentication
                await _jsRuntime.InvokeVoidAsync("console.log", $"CustomAuthStateProvider: User authenticated: {user.Email}");

                // Build claims and return
                var claims = new List<Claim>
                {
                    new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new(ClaimTypes.Name, user.Email),
                    new(ClaimTypes.Email, user.Email),
                    new("fullName", user.FullName)
                };

                foreach (var role in user.Roles)
                {
                    claims.Add(new(ClaimTypes.Role, role));
                }

                var identity = new ClaimsIdentity(claims, "jwt");
                var principal = new ClaimsPrincipal(identity);
                _cachedAuthState = new AuthenticationState(principal);

                return _cachedAuthState;
            }
            catch (InvalidOperationException ex)
            {
                // JavaScript interop not available yet, return anonymous state
                await _jsRuntime.InvokeVoidAsync("console.log", $"CustomAuthStateProvider: JS interop error: {ex.Message}");
                return _cachedAuthState;
            }
            catch (Exception ex)
            {
                await _jsRuntime.InvokeVoidAsync("console.error", $"CustomAuthStateProvider: Unexpected error: {ex.Message}");
                return _cachedAuthState;
            }
        }

        public async Task RefreshAuthStateAsync()
        {
            if (OperatingSystem.IsBrowser())
            {
                await _jsRuntime.InvokeVoidAsync("console.log", "CustomAuthStateProvider: Refreshing auth state");
                NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            }
        }

        #endregion Public Methods

        #region Private Methods

        private async void OnAuthStateChanged(bool isAuthenticated)
        {
            if (OperatingSystem.IsBrowser())
            {
                await _jsRuntime.InvokeVoidAsync("console.log", $"CustomAuthStateProvider: Auth state changed - Authenticated: {isAuthenticated}");
                NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            }
        }

        #endregion Private Methods
    }
}