namespace TricycleFareAndPassengerManagement.Application.Common.DTOs.Response
{
    public class Response
    {
        #region Properties

        public bool IsSuccess { get; set; }
        public Dictionary<string, string[]>? ValidationErrors { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
        public string Warning { get; set; } = string.Empty;
        public string Info { get; set; } = string.Empty;

        #endregion Properties
    }
}