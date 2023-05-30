using DayzMapsLoader.Core.Extensions;
using DayzMapsLoader.Infrastructure.Extensions;

using MediatR;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DayzMapsLoader.Tests.xUnit.Extensions;

internal static class ServiceCollectionExtensions
{
    public static IServiceProvider BuildCollection(this IServiceCollection services)
    {
        var connectionString = GetConnectionString();
        services.AddCoreLayer();
        services.AddInfrastructureLayer(connectionString);
        services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

        return services.BuildServiceProvider();
    }

    public static string GetConnectionString()
    {
        var pathConfigPath = Path.Combine(Directory.GetCurrentDirectory(), "Properties");
        var configuration = new ConfigurationBuilder()
            .SetBasePath(pathConfigPath)
            .AddJsonFile("appsettings.json")
            .Build();

        return configuration.GetConnectionString("DefaultConnection")!;
    }
}