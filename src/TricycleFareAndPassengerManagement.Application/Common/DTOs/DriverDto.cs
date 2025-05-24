namespace TricycleFareAndPassengerManagement.Application.Common.DTOs
{
    public class DriverDto
    {
        #region Properties

        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string LicenseNumber { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string TricycleNumber { get; set; } = string.Empty;
        public DateTime DateRegistered { get; set; }
        public bool IsActive { get; set; }

        #endregion Properties
    }
}