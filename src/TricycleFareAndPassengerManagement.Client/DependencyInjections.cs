using Microsoft.AspNetCore.Components.Authorization;
using TricycleFareAndPassengerManagement.Client.Security;
using TricycleFareAndPassengerManagement.Client.Services;
using TricycleFareAndPassengerManagement.Client.Services.Interfaces;

namespace TricycleFareAndPassengerManagement.Client
{
    public static class DependencyInjections
    {
        #region Public Methods

        public static IServiceCollection AddServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            AddPersistence(services, configuration);

            return services;
        }

        #endregion Public Methods

        #region Private Methods

        private static void AddPersistence(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("API"));
            services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
            services.AddScoped<CustomAuthStateProvider>();
            services.AddScoped<AuthorizationDelegatingHandler>();
            services.AddScoped<AuthenticationStateProvider>(provider =>
                provider.GetRequiredService<CustomAuthStateProvider>());

            services.AddScoped<ISecureStorageService, SecureStorageService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IDriverClientService, DriverClientService>();
            services.AddScoped<IPassengerClientService, PassengerClientService>();
            services.AddScoped<IFareClientService, FareClientService>();
            services.AddScoped<IReportClientService, ReportClientService>();
            services.AddScoped<ITripClientService, TripClientService>();
        }

        #endregion Private Methods
    }
}