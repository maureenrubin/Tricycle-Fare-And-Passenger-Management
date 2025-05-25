using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace TricycleFareAndPassengerManagement.Application
{
    public static class DependencyInjection
    {
        #region Public Methods

        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            return services;
        }

        #endregion Public Methods
    }
}