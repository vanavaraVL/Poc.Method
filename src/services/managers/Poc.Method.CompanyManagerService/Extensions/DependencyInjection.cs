using Microsoft.Extensions.DependencyInjection;
using Poc.Method.CompanyManager;

namespace Poc.Method.CompanyManagerService.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterCompanyManager(this IServiceCollection services)
        {
            services.AddTransient<ICompanyManager, CompanyManagerService>();

            return services;
        }
    }
}
