using TricycleFareAndPassengerManagement.Application.Common.DTOs;

namespace TricycleFareAndPassengerManagement.Client.Services.Interfaces
{
    public interface IFareClientService
    {
        Task<FareCalculationDto> CalculateFareAsync(double distance);
    }
}
