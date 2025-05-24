using TricycleFareAndPassengerManagement.Application.Common.DTOs;
using TricycleFareAndPassengerManagement.Client.Services.Interfaces;

namespace TricycleFareAndPassengerManagement.Client.Services
{
    public class ReportClientService(HttpClient _httpClient) : IReportClientService
    {
        public async Task<DailyReportDto> GetDailyReportAsync(DateTime date)
        {
            var response = await _httpClient.GetFromJsonAsync<DailyReportDto>($"api/Reports/reports/daily?date={date:yyyy-MM-dd}");
            return response ?? new DailyReportDto();
        }
    }
}
