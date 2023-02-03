using DayzMapsLoader.Domain.Entities;
using DayzMapsLoader.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace DayzMapsLoader.Infrastructure.Contexts;

public class DayzMapLoaderContext : DbContext
{
    public DayzMapLoaderContext()
    { }

    public DayzMapLoaderContext(DbContextOptions<DayzMapLoaderContext> options)
        : base(options)
    { }

    public DbSet<Map> Maps { get; set; }
    public DbSet<MapProvider> MapProviders { get; set; }
    public DbSet<ProvidedMap> ProvidedMaps { get; set; }
    public DbSet<MapType> MapTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Map>().
                Property(p => p.LastUpdate)
                .HasColumnType("date");

        modelBuilder.InitializeData();
    }
}