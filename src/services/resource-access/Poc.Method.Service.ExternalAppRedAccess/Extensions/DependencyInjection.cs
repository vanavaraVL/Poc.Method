using Microsoft.Extensions.DependencyInjection;
using Poc.Method.ExternalAppRedAccess;

namespace Poc.Method.Service.ExternalAppRedAccess.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterExternalApplicationRedResourceAccess(this IServiceCollection services)
        {
            services.AddTransient<IExternalAppRedAccess, ExternalAppRedAccessService>();

            return services;
        }
    }
}