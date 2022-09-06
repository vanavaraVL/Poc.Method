using Microsoft.Extensions.DependencyInjection;
using Poc.Method.ContextStorageAccess;

namespace Poc.Method.Service.ContextStorageAccess.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterContextStorageResourceAccess(this IServiceCollection services)
        {
            services.AddTransient<IContextStorageAccess, ContextStorageAccessService>();

            return services;
        }
    }
}