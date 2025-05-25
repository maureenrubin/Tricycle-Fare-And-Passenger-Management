using MediatR;
using TricycleFareAndPassengerManagement.Application.Common.DTOs;

namespace TricycleFareAndPassengerManagement.Application.Features.Passengers.Queries
{
    public class GetAllPassengersQuery : IRequest<List<PassengerDto>>
    {
    }
}