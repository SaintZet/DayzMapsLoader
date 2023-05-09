using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using DayzMapsLoader.Application.Abstractions.Infrastructure.Repositories;
using DayzMapsLoader.Application.Abstractions.Infrastructure.Services;
using DayzMapsLoader.Infrastructure.Contexts;
using DayzMapsLoader.Infrastructure.Repositories;
using DayzMapsLoader.Infrastructure.Services;

namespace DayzMapsLoader.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastractureLayer(this IServiceCollection services, string dbConnection)
    => services
        .AddRepositories()
        .AddServices()
        .AddDatabase(dbConnection);

    private static IServiceCollection AddRepositories(this IServiceCollection services)
        => services
            .AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>))
            .AddTransient<IMapProvidersRepository, MapProvidersRepository>()
            .AddTransient<IMapsRepository, MapsRepository>()
            .AddTransient<IProvidedMapsRepository, ProvidedMapsRepository>();

    private static IServiceCollection AddServices(this IServiceCollection services)
        => services.AddTransient<IMultipleThirdPartyApiService, MultipleThirdPartyApiService>();

    private static IServiceCollection AddDatabase(this IServiceCollection services, string dbConnection)
        => services.AddDbContext<DayzMapLoaderContext>(
            options => options
                        .EnableSensitiveDataLogging()
                        .UseSqlServer(dbConnection),
            ServiceLifetime.Transient);
}