namespace TricycleFareAndPassengerManagement.Domain.Entities
{
    public class Passenger
    {
        #region Properties

        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public DateTime DateRegistered { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;

        // Navigation
        public ICollection<Trip> Trips { get; set; } = new List<Trip>();

        #endregion Properties
    }
}