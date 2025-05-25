using TricycleFareAndPassengerManagement.Domain.Enums;

namespace TricycleFareAndPassengerManagement.Domain.Entities
{
    public class Trip
    {
        #region Properties

        public int Id { get; set; }
        public int DriverId { get; set; }
        public int PassengerId { get; set; }
        public string PickupLocation { get; set; } = string.Empty;
        public string DropoffLocation { get; set; } = string.Empty;
        public double Distance { get; set; } // in kilometers
        public decimal BaseFare { get; set; }
        public decimal TotalFare { get; set; }
        public DateTime TripDate { get; set; } = DateTime.Now;
        public TripStatus Status { get; set; } = TripStatus.Completed;

        // Navigation
        public Driver Driver { get; set; } = null!;

        public Passenger Passenger { get; set; } = null!;

        #endregion Properties
    }
}