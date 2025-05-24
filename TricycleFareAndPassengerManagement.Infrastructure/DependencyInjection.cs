using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TricycleFareAndPassengerManagement.Application.Common.Interfaces;
using TricycleFareAndPassengerManagement.Domain.Interfaces;
using TricycleFareAndPassengerManagement.Infrastructure.Data;
using TricycleFareAndPassengerManagement.Infrastructure.Repositories;
using TricycleFareAndPassengerManagement.Infrastructure.Services;

namespace TricycleFareAndPassengerManagement.Infrastructure
{
    public static class DependencyInjection
    {
        #region Public Methods

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Database
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();

            // Services
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenService, TokenService>();

            return services;
        }

        #endregion Public Methods
    }
}