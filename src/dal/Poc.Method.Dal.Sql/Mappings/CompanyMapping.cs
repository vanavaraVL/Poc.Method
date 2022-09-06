using Microsoft.EntityFrameworkCore;
using Poc.Method.Dal.Sql.Entities;
using Poc.Method.Dal.Sql.Mappings.Infrastructure;

namespace Poc.Method.Dal.Sql.Mappings
{
    internal class CompanyMapping : IMappingProfile
    {
        public void Map(ModelBuilder modelBuilder)
        {
            var ent = modelBuilder.Entity<CompanyEntity>();
            ent.HasKey(x => x.Id);
        }
    }
}
