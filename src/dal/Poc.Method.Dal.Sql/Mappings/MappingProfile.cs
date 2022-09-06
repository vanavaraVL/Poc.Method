using Microsoft.EntityFrameworkCore;
using Poc.Method.Dal.Sql.Mappings.Infrastructure;

namespace Poc.Method.Dal.Sql.Mappings
{
    internal static class MappingProfile
    {
        private static readonly List<IMappingProfile> ModelMappings;

        static MappingProfile()
        {
            ModelMappings = new List<IMappingProfile>();

            var type = typeof(IMappingProfile);
            var types = typeof(MappingProfile).Assembly
                .GetTypes()
                .Where(p => type.IsAssignableFrom(p) && p != type)
                .Distinct()
                .ToList();

            foreach (var t in types)
            {
                ModelMappings.Add((IMappingProfile)Activator.CreateInstance(t)!);
            }
        }

        public static void Map(ModelBuilder modelBuilder)
        {
            foreach (var modelMapping in ModelMappings)
            {
                modelMapping.Map(modelBuilder);
            }
        }
    }
}
