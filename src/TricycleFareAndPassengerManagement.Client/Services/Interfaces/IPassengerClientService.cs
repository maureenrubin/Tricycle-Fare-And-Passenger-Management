using TricycleFareAndPassengerManagement.Application.Common.DTOs;
using TricycleFareAndPassengerManagement.Application.Features.Passengers.Commands;

namespace TricycleFareAndPassengerManagement.Client.Services.Interfaces
{
    public interface IPassengerClientService
    {
        Task<List<PassengerDto>> GetAllPassengersAsync();


        Task<int> CreatePassengerAsync(CreatePassengerCommand command);


        Task<bool> UpdatePassengerAsync(int id, UpdatePassengerCommand command);
    }
}
