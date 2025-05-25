using MediatR;
using Microsoft.EntityFrameworkCore;
using TricycleFareAndPassengerManagement.Application.Common.DTOs;
using TricycleFareAndPassengerManagement.Domain.Interfaces;

namespace TricycleFareAndPassengerManagement.Application.Features.Trips.Queries
{
    public class GetAllTripsQueryHandler : IRequestHandler<GetAllTripsQuery, List<TripDto>>
    {
        #region Fields

        private readonly IAppDbContext _context;

        #endregion Fields

        #region Public Constructors

        public GetAllTripsQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<List<TripDto>> Handle(GetAllTripsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Trips
                .Include(t => t.Driver)
                .Include(t => t.Passenger)
                .Select(t => new TripDto
                {
                    Id = t.Id,
                    DriverId = t.DriverId,
                    DriverName = $"{t.Driver.FullName}",
                    TricycleNumber = t.Driver.TricycleNumber,
                    PassengerId = t.PassengerId,
                    PassengerName = $"{t.Passenger.FullName}",
                    PickupLocation = t.PickupLocation,
                    DropoffLocation = t.DropoffLocation,
                    Distance = t.Distance,
                    BaseFare = t.BaseFare,
                    TotalFare = t.TotalFare,
                    TripDate = t.TripDate,
                    Status = t.Status
                })
                .OrderByDescending(t => t.TripDate)
                .ToListAsync(cancellationToken);
        }

        #endregion Public Methods
    }
}