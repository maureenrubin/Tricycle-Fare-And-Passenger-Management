using TricycleFareAndPassengerManagement.Application.Common.DTOs;
using TricycleFareAndPassengerManagement.Application.Features.Trips.Commands;
using TricycleFareAndPassengerManagement.Client.Services.Interfaces;

namespace TricycleFareAndPassengerManagement.Client.Services
{
    public class TripClientService(HttpClient _httpClient) : ITripClientService
    {
        #region Public Methods

        public async Task<List<TripDto>> GetAllTripsAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<TripDto>>("api/Trip/getall");
            return response ?? new List<TripDto>();
        }

        public async Task<int> CreateTripAsync(CreateTripCommand command)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Trip/create", command);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<int>();
        }

        #endregion Public Methods
    }
}