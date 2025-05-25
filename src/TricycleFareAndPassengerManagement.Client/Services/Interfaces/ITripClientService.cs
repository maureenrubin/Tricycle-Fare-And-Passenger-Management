using TricycleFareAndPassengerManagement.Application.Common.DTOs;
using TricycleFareAndPassengerManagement.Application.Features.Trips.Commands;

namespace TricycleFareAndPassengerManagement.Client.Services.Interfaces
{
    public interface ITripClientService
    {
        Task<List<TripDto>> GetAllTripsAsync();

        Task<int> CreateTripAsync(CreateTripCommand command);

    }
}
