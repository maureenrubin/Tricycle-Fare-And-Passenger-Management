using TricycleFareAndPassengerManagement.Application.Common.DTOs;

namespace TricycleFareAndPassengerManagement.Client.Models
{
    public class AuthResult
    {
        #region Properties

        public bool Success { get; set; }
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? ExpiresAt { get; set; }
        public UserDto? User { get; set; }
        public List<string> Errors { get; set; } = new();

        #endregion Properties
    }
}