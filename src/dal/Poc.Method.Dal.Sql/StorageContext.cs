using Microsoft.EntityFrameworkCore;
using Poc.Method.Dal.Sql.Entities;
using Poc.Method.Dal.Sql.Mappings;

namespace Poc.Method.Dal.Sql
{
    public class StorageContext : DbContext
    {
        public DbSet<PersonEntity> Persons { get; set; }

        public DbSet<CompanyEntity> Companies { get; set; }

        public StorageContext(DbContextOptions<StorageContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            MappingProfile.Map(modelBuilder);
        }
    }
}