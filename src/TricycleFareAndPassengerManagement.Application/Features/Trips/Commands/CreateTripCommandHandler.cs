using MediatR;
using TricycleFareAndPassengerManagement.Domain.Entities;
using TricycleFareAndPassengerManagement.Domain.Enums;
using TricycleFareAndPassengerManagement.Domain.Interfaces;

namespace TricycleFareAndPassengerManagement.Application.Features.Trips.Commands
{
    public class CreateTripCommandHandler : IRequestHandler<CreateTripCommand, int>
    {
        #region Fields

        private readonly IAppDbContext _context;

        #endregion Fields

        #region Public Constructors

        public CreateTripCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<int> Handle(CreateTripCommand request, CancellationToken cancellationToken)
        {
            // Simple fare calculation
            var baseFare = 15.00m;
            var perKmRate = 8.50m;
            var totalFare = baseFare + (decimal) (request.Distance * (double) perKmRate);

            var trip = new Trip
            {
                DriverId = request.DriverId,
                PassengerId = request.PassengerId,
                PickupLocation = request.PickupLocation,
                DropoffLocation = request.DropoffLocation,
                Distance = request.Distance,
                BaseFare = baseFare,
                TotalFare = totalFare,
                Status = TripStatus.Completed
            };

            _context.Trips.Add(trip);
            await _context.SaveChangesAsync(cancellationToken);

            return trip.Id;
        }

        #endregion Public Methods
    }
}