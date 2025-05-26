using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;
using TricycleFareAndPassengerManagement.Client.Models;

namespace TricycleFareAndPassengerManagement.Client.Security
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        #region Fields

        private const string TokenKey = "authToken";
        private const string UserKey = "currentUser";
        private readonly ILocalStorageService _localStorage;
        private readonly ILogger<CustomAuthStateProvider> _logger;
        private readonly TaskCompletionSource<bool> _loadingTask = new();
        private AuthenticationState? _cachedAuthState;

        #endregion Fields

        #region Public Constructors

        public CustomAuthStateProvider(ILocalStorageService localStorage, ILogger<CustomAuthStateProvider> logger)
        {
            _localStorage = localStorage;
            _logger = logger;
        }

        #endregion Public Constructors

        #region Public Methods

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            // Return cached state if available and not expired
            if (_cachedAuthState != null)
            {
                return _cachedAuthState;
            }

            try
            {
                var token = await GetTokenSafelyAsync();
                var userJson = await GetUserDataSafelyAsync();

                if (string.IsNullOrWhiteSpace(token) || string.IsNullOrWhiteSpace(userJson))
                {
                    _cachedAuthState = CreateAnonymousState();
                    return _cachedAuthState;
                }

                var user = JsonSerializer.Deserialize<UserDto>(userJson);
                if (user == null)
                {
                    _cachedAuthState = CreateAnonymousState();
                    return _cachedAuthState;
                }

                // Validate token expiration
                if (IsTokenExpired(token))
                {
                    await ClearAuthDataAsync();
                    _cachedAuthState = CreateAnonymousState();
                    return _cachedAuthState;
                }

                var claims = BuildClaims(token, user);
                var identity = new ClaimsIdentity(claims, "jwt");
                var principal = new ClaimsPrincipal(identity);

                _cachedAuthState = new AuthenticationState(principal);
                return _cachedAuthState;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAuthenticationStateAsync");
                _cachedAuthState = CreateAnonymousState();
                return _cachedAuthState;
            }
        }

        public async Task NotifyUserAuthentication()
        {
            // Clear cached state to force refresh
            _cachedAuthState = null;

            // Small delay to ensure localStorage is ready
            await Task.Delay(100);

            var authState = await GetAuthenticationStateAsync();
            NotifyAuthenticationStateChanged(Task.FromResult(authState));
        }

        public async Task NotifyUserLogout()
        {
            await ClearAuthDataAsync();
            _cachedAuthState = null;
            NotifyAuthenticationStateChanged(Task.FromResult(CreateAnonymousState()));
        }

        #endregion Public Methods

        #region Private Methods

        private async Task<string?> GetTokenSafelyAsync()
        {
            try
            {
                // Add timeout for localStorage operations
                using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
                var task = _localStorage.GetItemAsync<string>(TokenKey);
                return await task.AsTask().WaitAsync(cts.Token);
            }
            catch (InvalidOperationException)
            {
                // JavaScript interop not available (prerendering)
                _logger.LogDebug("JavaScript interop not available for token retrieval");
                return null;
            }
            catch (TaskCanceledException)
            {
                _logger.LogWarning("Timeout occurred while getting token from localStorage");
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to get token from localStorage");
                return null;
            }
        }

        private async Task<string?> GetUserDataSafelyAsync()
        {
            try
            {
                // Add timeout for localStorage operations
                using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
                var task = _localStorage.GetItemAsync<string>(UserKey);
                return await task.AsTask().WaitAsync(cts.Token);
            }
            catch (InvalidOperationException)
            {
                // JavaScript interop not available (prerendering)
                _logger.LogDebug("JavaScript interop not available for user data retrieval");
                return null;
            }
            catch (TaskCanceledException)
            {
                _logger.LogWarning("Timeout occurred while getting user data from localStorage");
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to get user data from localStorage");
                return null;
            }
        }

        private bool IsTokenExpired(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadJwtToken(token);
                return jwtToken.ValidTo <= DateTime.UtcNow.AddMinutes(-1); // Add 1 minute buffer
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to validate token expiration");
                return true; // If we can't read the token, consider it expired
            }
        }

        private List<Claim> BuildClaims(string token, UserDto user)
        {
            var claims = new List<Claim>();

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadJwtToken(token);

                // Add claims from token (except roles)
                foreach (var claim in jwtToken.Claims)
                {
                    if (claim.Type != ClaimTypes.Role && claim.Type != "role")
                    {
                        claims.Add(claim);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to parse JWT token claims");
            }

            // Add user info claims
            claims.Add(new Claim(ClaimTypes.Name, user.FullName ?? string.Empty));
            claims.Add(new Claim(ClaimTypes.Email, user.Email ?? string.Empty));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));

            // Add all roles as separate claims
            if (user.Roles != null)
            {
                foreach (var role in user.Roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
            }

            return claims;
        }

        private AuthenticationState CreateAnonymousState()
        {
            var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
            return new AuthenticationState(anonymousUser);
        }

        private async Task ClearAuthDataAsync()
        {
            try
            {
                var tasks = new[]
                {
                    _localStorage.RemoveItemAsync(TokenKey).AsTask(),
                    _localStorage.RemoveItemAsync(UserKey).AsTask()
                };

                using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
                await Task.WhenAll(tasks).WaitAsync(cts.Token);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to clear auth data from localStorage");
            }
        }

        #endregion Private Methods
    }
}