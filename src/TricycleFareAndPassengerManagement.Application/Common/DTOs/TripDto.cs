using TricycleFareAndPassengerManagement.Domain.Enums;

namespace TricycleFareAndPassengerManagement.Application.Common.DTOs
{
    public class TripDto
    {
        #region Properties

        public int Id { get; set; }
        public int DriverId { get; set; }
        public string DriverName { get; set; } = string.Empty;
        public string TricycleNumber { get; set; } = string.Empty;
        public int PassengerId { get; set; }
        public string PassengerName { get; set; } = string.Empty;
        public string PickupLocation { get; set; } = string.Empty;
        public string DropoffLocation { get; set; } = string.Empty;
        public double Distance { get; set; }
        public decimal BaseFare { get; set; }
        public decimal TotalFare { get; set; }
        public DateTime TripDate { get; set; }
        public TripStatus Status { get; set; }

        #endregion Properties
    }
}