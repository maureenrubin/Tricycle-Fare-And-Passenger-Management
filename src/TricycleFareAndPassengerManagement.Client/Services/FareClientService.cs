using TricycleFareAndPassengerManagement.Application.Common.DTOs;
using TricycleFareAndPassengerManagement.Client.Services.Interfaces;

namespace TricycleFareAndPassengerManagement.Client.Services
{
    public class FareClientService(HttpClient _httpClient) : IFareClientService
    {
        public async Task<FareCalculationDto> CalculateFareAsync(double distance)
        {
            var query = new { Distance = distance };
            var response = await _httpClient.PostAsJsonAsync("api/Fare/fares/calculate", query);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<FareCalculationDto>() ?? new FareCalculationDto();
        }

        // Reports
        public async Task<DailyReportDto> GetDailyReportAsync(DateTime date)
        {
            var response = await _httpClient.GetFromJsonAsync<DailyReportDto>($"api/Reports/reports/daily?date={date:yyyy-MM-dd}");
            return response ?? new DailyReportDto();
        }
    }
}
