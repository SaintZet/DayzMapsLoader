using DayzMapsLoader.Application.Abstractions.Services;
using DayzMapsLoader.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DayzMapsLoader.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        => services
            .AddTransient<IMapDownloadArchiveService, MapDownloadArchiveService>()
            .AddTransient<IMapDownloadImageService, MapDownloadImageService>();
}