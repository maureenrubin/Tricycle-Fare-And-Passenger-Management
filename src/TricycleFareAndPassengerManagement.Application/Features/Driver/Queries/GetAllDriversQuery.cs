using MediatR;
using TricycleFareAndPassengerManagement.Application.Common.DTOs;

namespace TricycleFareAndPassengerManagement.Application.Features.Driver.Queries
{
    public class GetAllDriversQuery : IRequest<List<DriverDto>>
    {
    }
}