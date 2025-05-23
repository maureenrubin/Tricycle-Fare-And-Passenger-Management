using System.ComponentModel.DataAnnotations;

namespace TricyleFareAndPassengerManagementSystem.Api.Models
{
    public class User
    {
        #region Properties

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        #endregion Properties
    }
}