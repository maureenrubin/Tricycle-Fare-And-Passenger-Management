using TricycleFareAndPassengerManagement.Application.Common.DTOs;
using TricycleFareAndPassengerManagement.Client.Services.Interfaces;

namespace TricycleFareAndPassengerManagement.Client.Services
{
    public class ReportClientService(HttpClient _httpClient) : IReportClientService
    {
        #region Public Methods

        public async Task<DailyReportDto> GetDailyReportAsync(DateTime date)
        {
            var response = await _httpClient.GetFromJsonAsync<DailyReportDto>($"api/Reports/getreports?date={date:yyyy-MM-dd}");
            return response ?? new DailyReportDto();
        }

        #endregion Public Methods
    }
}