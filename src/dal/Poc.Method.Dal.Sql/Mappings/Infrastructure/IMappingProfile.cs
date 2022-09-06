using Microsoft.EntityFrameworkCore;

namespace Poc.Method.Dal.Sql.Mappings.Infrastructure
{
    internal interface IMappingProfile
    {
        void Map(ModelBuilder modelBuilder);
    }
}
