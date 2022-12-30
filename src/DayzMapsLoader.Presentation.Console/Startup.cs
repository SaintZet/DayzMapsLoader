using DayzMapsLoader.Application.Abstractions.Infrastructure;
using DayzMapsLoader.Application.Abstractions.Services;
using DayzMapsLoader.Application.Services;
using DayzMapsLoader.Infrastructure.DbContexts;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DayzMapsLoader.Presentation.ConsoleApp
{
    internal class Startup
    {
        public IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection()
            .AddSingleton<IMapsDbContext, JsonMapsDbContext>()

            .AddSingleton<IMapDownloader>(x => new MapDownloader(x.GetRequiredService<IMapsDbContext>()))
            .AddSingleton<IMapSaver>(x => new MapSaver(x.GetRequiredService<IMapsDbContext>()))
            .AddSingleton<IMapMerger>(x => new MapMerger(x.GetRequiredService<IMapsDbContext>()))

            .AddSingleton(s => new Application(
                s.GetRequiredService<IMapDownloader>(),
                s.GetRequiredService<IMapSaver>(),
                s.GetRequiredService<IMapMerger>())
            );

            return services.BuildServiceProvider();
        }
    }
}