using MediatR;
using TricycleFareAndPassengerManagement.Application.Common.DTOs;

namespace TricycleFareAndPassengerManagement.Application.Features.Trips.Queries
{
    public class GetAllTripsQuery : IRequest<List<TripDto>>
    {
    }
}