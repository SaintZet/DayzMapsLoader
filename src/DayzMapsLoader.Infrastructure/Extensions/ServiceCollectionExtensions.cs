using DayzMapsLoader.Application.Abstractions.Infrastructure.Repositories;
using DayzMapsLoader.Application.Abstractions.Infrastructure.Services;
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
    => services.AddRepositories().AddServices();

    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        string conncection = configuration.GetConnectionString("DefaultConnection")!;

        return services.AddDbContext<DayzMapLoaderContext>(
            options => options
                        .EnableSensitiveDataLogging()
                        .UseSqlServer(conncection));
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
                => services
        .AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>))
        .AddTransient<IMapProvidersRepository, MapProvidersRepository>()
        .AddTransient<IMapsRepository, MapsRepository>()
        .AddTransient<IProvidedMapsRepository, ProvidedMapsRepository>();

    private static IServiceCollection AddServices(this IServiceCollection services)
        => services.AddTransient<IMultipleThirdPartyApiService, MultipleThirdPartyApiService>();
}