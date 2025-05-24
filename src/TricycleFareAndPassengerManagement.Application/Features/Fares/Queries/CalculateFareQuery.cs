using MediatR;
using TricycleFareAndPassengerManagement.Application.Common.DTOs;

namespace TricycleFareAndPassengerManagement.Application.Features.Fares.Queries
{
    public class CalculateFareQuery : IRequest<FareCalculationDto>
    {
        #region Properties

        public double Distance { get; set; }

        #endregion Properties
    }
}