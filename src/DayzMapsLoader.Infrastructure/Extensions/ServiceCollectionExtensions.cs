using DayzMapsLoader.Application.Abstractions.Infrastructure;
using DayzMapsLoader.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DayzMapsLoader.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastractureLayer(this IServiceCollection services)
    => services
        .AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>))
        .AddTransient<IMapProvidersRepository, MapProvidersRepository>()
        .AddTransient<IMapsRepository, MapsRepository>()
        .AddTransient<IProvidedMapsRepository, ProvidedMapsRepository>();
}