using TricycleFareAndPassengerManagement.Application.Common.Models;

namespace TricycleFareAndPassengerManagement.Application.Common.Interfaces
{
    public interface IAuthService
    {
        #region Private Methods

        Task<AuthResult> LoginAsync(string email, string password);

        Task<AuthResult> RegisterAsync(string fullName, string email, string password);

        Task<AuthResult> RefreshTokenAsync(string refreshToken);

        Task<bool> RevokeTokenAsync(string refreshToken);

        #endregion Private Methods
    }
}