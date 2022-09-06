using Microsoft.EntityFrameworkCore;
using Poc.Method.Dal.Sql.Entities;
using Poc.Method.Dal.Sql.Mappings.Infrastructure;

namespace Poc.Method.Dal.Sql.Mappings
{
    internal class PersonMapping: IMappingProfile
    {
        public void Map(ModelBuilder modelBuilder)
        {
            var ent = modelBuilder.Entity<PersonEntity>();
            ent.HasKey(x => x.Id);

            ent.HasOne(x => x.Employer)
                .WithMany(x => x.Employees)
                .HasForeignKey(x => x.EmployerId).IsRequired(false);
        }
    }
}
