namespace TricycleFareAndPassengerManagement.Client.Models
{
    public class ApiResponse<T>
    {
        #region Properties

        public bool Success { get; set; }
        public T? Data { get; set; }
        public string? Message { get; set; }
        public List<string> Errors { get; set; } = new();

        #endregion Properties
    }
}