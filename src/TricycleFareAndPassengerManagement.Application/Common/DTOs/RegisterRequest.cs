namespace TricycleFareAndPassengerManagement.Application.Common.DTOs
{
    public class RegisterRequest
    {
        #region Properties

        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        #endregion Properties
    }
}