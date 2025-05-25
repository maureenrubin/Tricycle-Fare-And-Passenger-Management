using MediatR;
using TricycleFareAndPassengerManagement.Application.Common.DTOs;

namespace TricycleFareAndPassengerManagement.Application.Features.Fares.Queries
{
    public class CalculateFareQueryHandler : IRequestHandler<CalculateFareQuery, FareCalculationDto>
    {
        #region Public Methods

        public Task<FareCalculationDto> Handle(CalculateFareQuery request, CancellationToken cancellationToken)
        {
            var baseFare = 15.00m; // Base fare
            var perKmRate = 8.50m; // Rate per kilometer
            var totalFare = baseFare + (decimal) (request.Distance * (double) perKmRate);

            var result = new FareCalculationDto
            {
                Distance = request.Distance,
                BaseFare = baseFare,
                PerKmRate = perKmRate,
                TotalFare = Math.Round(totalFare, 2)
            };

            return Task.FromResult(result);
        }

        #endregion Public Methods
    }
}