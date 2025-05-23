using System.ComponentModel.DataAnnotations;

namespace TricyleFareAndPassengerManagementSystem.Api.DTOs
{
    public class LoginRequest
    {
        #region Properties

        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        #endregion Properties
    }
}