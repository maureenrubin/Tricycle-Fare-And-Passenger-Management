namespace TricycleFareAndPassengerManagement.Application.Common.DTOs
{
    public class PassengerDto
    {
        #region Properties

        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public DateTime DateRegistered { get; set; }
        public bool IsActive { get; set; }

        #endregion Properties
    }
}