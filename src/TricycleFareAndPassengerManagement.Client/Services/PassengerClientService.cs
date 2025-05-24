using TricycleFareAndPassengerManagement.Application.Common.DTOs;
using TricycleFareAndPassengerManagement.Application.Features.Passengers.Commands;
using TricycleFareAndPassengerManagement.Client.Services.Interfaces;

namespace TricycleFareAndPassengerManagement.Client.Services
{
    public class PassengerClientService(HttpClient _httpClient) : IPassengerClientService
    {
        public async Task<List<PassengerDto>> GetAllPassengersAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<PassengerDto>>("api/Passengers/passengers");
            return response ?? new List<PassengerDto>();
        }

        public async Task<int> CreatePassengerAsync(CreatePassengerCommand command)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Passengers/passengers", command);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<int>();
        }

        public async Task<bool> UpdatePassengerAsync(int id, UpdatePassengerCommand command)
        {
            command.Id = id;
            var response = await _httpClient.PutAsJsonAsync($"api/Passengers/passengers/{id}", command);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<bool>();
        }
    }
}
