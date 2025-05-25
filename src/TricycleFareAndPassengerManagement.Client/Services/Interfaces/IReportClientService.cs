using TricycleFareAndPassengerManagement.Application.Common.DTOs;

namespace TricycleFareAndPassengerManagement.Client.Services.Interfaces
{
    public interface IReportClientService
    {
        Task<DailyReportDto> GetDailyReportAsync(DateTime date);
    }
}
