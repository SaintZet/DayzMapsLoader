using DayzMapsLoader.Application.Abstractions.Infrastructure;
using DayzMapsLoader.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Reflection.Metadata;

namespace DayzMapsLoader.Infrastructure.Contexts;

public class DayzMapLoaderContext : DbContext
{
    public DayzMapLoaderContext()
    {
    }

    public DayzMapLoaderContext(DbContextOptions<DayzMapLoaderContext> options)
        : base(options)
    {
    }

    public DbSet<Map> Maps { get; set; }
    public DbSet<MapProvider> MapProviders { get; set; }
    public DbSet<ProvidedMap> ProvidedMaps { get; set; }
    public DbSet<MapType> MapTypes { get; set; }

    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    modelBuilder.Entity<MapProvider>()
    //        .HasKey(b => b.Id);

    //    modelBuilder.Entity<MapType>()
    //        .HasKey(b => b.Id);

    //    modelBuilder.Entity<ProvidedMap>()
    //        .HasKey(b => b.Id)
    //        .;
    //}
}