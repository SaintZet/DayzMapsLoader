using DayzMapsLoader.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace DayzMapsLoader.Infrastructure.Contexts
{
    public class DayzMapLoaderContext : DbContext
    {
        public DayzMapLoaderContext(DbContextOptions<DayzMapLoaderContext> options)
            : base(options)
        {
        }

        public DbSet<DayzMap> DayzMaps { get; set; }
        public DbSet<MapProvider> MapProviders { get; set; }
        public DbSet<ProvidersMapAsset> ProvidersMapAssets { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DayzMapLoader;Integrated Security=True;");
        //}
    }
}