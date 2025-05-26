using TricycleFareAndPassengerManagement.Application.Common.DTOs;
using TricycleFareAndPassengerManagement.Application.Features.Driver.Commands;

namespace TricycleFareAndPassengerManagement.Client.Services.Interfaces
{
    public interface IDriverClientService
    {
        Task<List<DriverDto>> GetAllDriversAsync();


        Task<DriverDto?> GetDriverByIdAsync(int id);


        Task<int> CreateDriverAsync(CreateDriverCommand command);


        Task<bool> UpdateDriverAsync(int id, UpdateDriverCommand command);


        Task<bool> DeleteDriverAsync(int id);

    }
}
