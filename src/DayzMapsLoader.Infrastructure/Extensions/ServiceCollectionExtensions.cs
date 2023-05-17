using DayzMapsLoader.Core.Contracts.Infrastructure.Repositories;
using DayzMapsLoader.Core.Contracts.Infrastructure.Services;

using DayzMapsLoader.Infrastructure.Contexts;
using DayzMapsLoader.Infrastructure.Repositories;
using DayzMapsLoader.Infrastructure.Services;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DayzMapsLoader.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastractureLayer(this IServiceCollection services)
        => services
            .AddRepositories()
            .AddServices()
            .AddDatabase();

    private static IServiceCollection AddRepositories(this IServiceCollection services)
        => services
            .AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>))
            .AddTransient<IMapProvidersRepository, MapProvidersRepository>()
            .AddTransient<IMapsRepository, MapsRepository>()
            .AddTransient<IProvidedMapsRepository, ProvidedMapsRepository>();

    private static IServiceCollection AddServices(this IServiceCollection services)
        => services
            .AddTransient<IMultipleThirdPartyApiService, MultipleThirdPartyApiService>()
            .AddTransient<IFileService, FileService>();

    private static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var dbConnection = config.GetConnectionString("DefaultConnection")!;

        return services.AddDbContext<DayzMapLoaderContext>(
                options => options
                            .EnableSensitiveDataLogging()
                            .UseSqlServer(dbConnection),
                ServiceLifetime.Transient);
    }
}