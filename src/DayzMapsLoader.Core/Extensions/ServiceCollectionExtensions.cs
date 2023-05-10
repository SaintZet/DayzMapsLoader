using DayzMapsLoader.Core.Contracts.Services;
using DayzMapsLoader.Core.Services;

using Microsoft.Extensions.DependencyInjection;

namespace DayzMapsLoader.Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        => services
            .AddTransient<IFileService, FileService>()
            .AddTransient<IMapDownloadArchiveService, MapDownloadArchiveService>()
            .AddTransient<IMapDownloadImageService, MapDownloadImageService>();
}