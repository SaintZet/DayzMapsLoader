using DayzMapsLoader.Application.Abstractions;
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
            .AddSingleton(s => new Application(
                s.GetRequiredService<IMapDownloader>())
            );

            return services.BuildServiceProvider();
        }
    }
}