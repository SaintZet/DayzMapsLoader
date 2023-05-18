using DayzMapsLoader.Core.Extensions;
using DayzMapsLoader.Infrastructure.Extensions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DayzMapsLoader.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureApplication(this IServiceCollection services)
        => services
            .AddCoreLayer()
            .AddInfrastractureLayer()
            .AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
}