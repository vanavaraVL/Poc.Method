using Microsoft.Extensions.DependencyInjection;
using Poc.Method.ExternalAppYellowAccess;

namespace Poc.Method.Service.ExternalAppYellowAccess.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterExternalApplicationYellowResourceAccess(this IServiceCollection services)
        {
            services.AddTransient<IExternalAppYellowAccess, ExternalAppYellowAccessService>();

            return services;
        }
    }
}