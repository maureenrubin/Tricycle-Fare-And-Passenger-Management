using MediatR;
using Microsoft.EntityFrameworkCore;
using TricycleFareAndPassengerManagement.Application.Common.DTOs;
using TricycleFareAndPassengerManagement.Domain.Interfaces;

namespace TricycleFareAndPassengerManagement.Application.Features.Reports.Queries
{
    public class GetDailyReportQueryHandler : IRequestHandler<GetDailyReportQuery, DailyReportDto>
    {
        #region Fields

        private readonly IAppDbContext _context;

        #endregion Fields

        #region Public Constructors

        public GetDailyReportQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<DailyReportDto> Handle(GetDailyReportQuery request, CancellationToken cancellationToken)
        {
            var date = request.Date.Date;
            var nextDay = date.AddDays(1);

            var trips = await _context.Trips
                .Where(t => t.TripDate >= date && t.TripDate < nextDay)
                .ToListAsync(cancellationToken);

            var activeDrivers = await _context.Drivers
                .CountAsync(d => d.IsActive, cancellationToken);

            var activePassengers = await _context.Passengers
                .CountAsync(p => p.IsActive, cancellationToken);

            return new DailyReportDto
            {
                Date = date,
                TotalTrips = trips.Count,
                TotalRevenue = trips.Sum(t => t.TotalFare),
                ActiveDrivers = activeDrivers,
                ActivePassengers = activePassengers
            };
        }

        #endregion Public Methods
    }
}