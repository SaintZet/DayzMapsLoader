//using DayzMapsLoader.Application.Abstractions.Infrastructure;
//using DayzMapsLoader.Application.Abstractions.Services;
//using DayzMapsLoader.Application.Services;
//using DayzMapsLoader.Infrastructure.Contexts;
//using DayzMapsLoader.Infrastructure.DbContexts;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using System;

//namespace DayzMapsLoader.Presentation.ConsoleApp
//{
//    internal class Startup
//    {
//        public IServiceProvider ConfigureServices()
//        {
//            var services = new ServiceCollection()
//                .AddDbContext<DayzMapLoaderContext>(options => options
//                .EnableSensitiveDataLogging().UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DayzMapLoader;Integrated Security=True;"))

//            .AddSingleton<IMapDownloader>(x => new MapDownloader(x.GetRequiredService<IMapsDbContext>()))
//            .AddSingleton<IMapSaver>(x => new MapSaver(x.GetRequiredService<IMapsDbContext>()))
//            .AddSingleton<IMapMerger>(x => new MapMerger(x.GetRequiredService<IMapsDbContext>()))

//            .AddSingleton(s => new Application(
//                s.GetRequiredService<IMapDownloader>(),
//                s.GetRequiredService<IMapSaver>(),
//                s.GetRequiredService<IMapMerger>())
//            );

//            return services.BuildServiceProvider();
//        }
//    }
//}