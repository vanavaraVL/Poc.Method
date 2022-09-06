using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Poc.Method.Dal.Sql.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterStorageContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("StorageContext");
            
            services.AddDbContext<StorageContext>(options => options.UseSqlServer(connectionString,
                sqlBuilder =>
                {
                    sqlBuilder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                    sqlBuilder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                }));

            return services;
        }
    }
}
