namespace TricyleFareAndPassengerManagementSystem.Api.DTOs
{
    public class AuthResponse
    {
        #region Properties

        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public string? Token { get; set; }
        public string? Username { get; set; }

        #endregion Properties
    }
}