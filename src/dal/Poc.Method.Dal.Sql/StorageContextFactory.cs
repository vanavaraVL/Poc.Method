using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Poc.Method.Dal.Sql
{
    class StorageContextFactory : IDesignTimeDbContextFactory<StorageContext>
    {
        public StorageContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<StorageContext>();
            
            optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS; Database=StorageContext; Trusted_Connection=True;");
            
            return new StorageContext(optionsBuilder.Options);
        }
    }
}