using Microsoft.Extensions.DependencyInjection;
using Poc.Method.PersonManager;

namespace Poc.Method.PersonManagerService.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterPersonManager(this IServiceCollection services)
        {
            services.AddTransient<IPersonManager, PersonManagerService>();

            return services;
        }
    }
}
