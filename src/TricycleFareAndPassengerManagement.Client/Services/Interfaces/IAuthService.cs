using TricycleFareAndPassengerManagement.Client.Models;

namespace TricycleFareAndPassengerManagement.Client.Services.Interfaces
{
    public interface IAuthService
    {
        #region Events

        event Action<bool>? AuthStateChanged;

        #endregion Events

        #region Public Methods

        Task<AuthResult> LoginAsync(LoginModel model);

        Task<AuthResult> RegisterAsync(RegisterModel model);

        Task<UserDto?> GetCurrentUserAsync();

        Task LogoutAsync();

        Task<bool> IsAuthenticatedAsync();

        Task<string?> GetTokenAsync();

        #endregion Public Methods
    }
}