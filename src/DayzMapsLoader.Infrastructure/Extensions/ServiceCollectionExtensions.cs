﻿using DayzMapsLoader.Core.Contracts.Infrastructure.Repositories;
using DayzMapsLoader.Core.Contracts.Infrastructure.Services;

using DayzMapsLoader.Infrastructure.Contexts;
using DayzMapsLoader.Infrastructure.Repositories;
using DayzMapsLoader.Infrastructure.Services;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DayzMapsLoader.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, string dbConnection)
        => services
            .AddRepositories()
            .AddServices()
            .AddDatabase(dbConnection);

    internal static IServiceCollection AddRepositories(this IServiceCollection services)
        => services
            .AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>))
            .AddTransient<IMapProvidersRepository, MapProvidersRepository>()
            .AddTransient<IMapsRepository, MapsRepository>()
            .AddTransient<IProvidedMapsRepository, ProvidedMapsRepository>();

    internal static IServiceCollection AddServices(this IServiceCollection services)
        => services
            .AddTransient<IMultipleThirdPartyApiService, MultipleThirdPartyApiService>()
            .AddTransient<IFileService, FileService>();

    internal static IServiceCollection AddDatabase(this IServiceCollection services, string dbConnection)
    {
        var dbContextOptions = new DbContextOptionsBuilder<DayzMapLoaderContext>()
            .EnableSensitiveDataLogging()
            .UseSqlServer(dbConnection)
            .Options;

        using (var dbContext = new DayzMapLoaderContext(dbContextOptions))
            EnsureDatabaseUpdated(dbContext);

        return services.AddDbContext<DayzMapLoaderContext>(
                options => options
                            .EnableSensitiveDataLogging()
                            .UseSqlServer(dbConnection),
                ServiceLifetime.Transient);
    }

    internal static void EnsureDatabaseUpdated(DbContext dbContext)
    {
        var appliedMigrations = dbContext.GetService<IHistoryRepository>()
            .GetAppliedMigrations()
            .Select(m => m.MigrationId);

        var allMigrations = dbContext.GetService<IMigrationsAssembly>()
            .Migrations
            .Select(m => m.Key);

        var pendingMigrations = allMigrations.Except(appliedMigrations);

        if (pendingMigrations.Any())
        {
            Console.WriteLine("Applying pending migrations...");
            dbContext.Database.Migrate();
            Console.WriteLine("Database updated successfully.");
        }
        else
        {
            Console.WriteLine("No pending migrations found.");
        }
    }
}