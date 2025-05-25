using MediatR;

namespace TricycleFareAndPassengerManagement.Application.Features.Trips.Commands
{
    public class CreateTripCommand : IRequest<int>
    {
        #region Properties

        public int DriverId { get; set; }
        public int PassengerId { get; set; }
        public string PickupLocation { get; set; } = string.Empty;
        public string DropoffLocation { get; set; } = string.Empty;
        public double Distance { get; set; }

        #endregion Properties
    }
}