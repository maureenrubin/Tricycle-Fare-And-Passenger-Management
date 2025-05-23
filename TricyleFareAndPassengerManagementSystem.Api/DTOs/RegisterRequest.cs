using System.ComponentModel.DataAnnotations;

namespace TricyleFareAndPassengerManagementSystem.Api.DTOs
{
    public class RegisterRequest
    {
        #region Properties

        [Required]
        [StringLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(6)]
        public string Password { get; set; } = string.Empty;

        #endregion Properties
    }
}