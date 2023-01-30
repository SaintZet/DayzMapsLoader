using DayzMapsLoader.Application.Abstractions.Infrastructure;
using DayzMapsLoader.Application.Abstractions.Services;
using DayzMapsLoader.Application.Services;
using DayzMapsLoader.Infrastructure.Contexts;
using DayzMapsLoader.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace DayzMapsLoader.Presentation.WebApi.Extensions
{
    internal static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
            => services
                .AddEndpointsApiExplorer()
                .AddSwaggerGen(options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "DayzMapLoader API",
                        Description = "An ASP.NET Core Web API for managing DayzMapLoader.",
                        Contact = new OpenApiContact
                        {
                            Name = "Chepets Serhii",
                            Url = new Uri("https://www.linkedin.com/in/serhii-chepets")
                        },
                        License = new OpenApiLicense
                        {
                            Name = "Example License",
                            Url = new Uri("https://github.com/SaintZet/DayzMapsLoader/blob/master/LICENSE")
                        }
                    });

                    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
                });

        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
            => services.AddTransient<IMapDownloadService, MapDownloadService>();

        public static IServiceCollection AddInfrastractureLayer(this IServiceCollection services, IConfiguration configuration)
            => services
                .AddDatabase(configuration)
                .AddTransient(typeof(IRepository<>), typeof(Repository<>))
                .AddTransient<IMapProvidersRepository, MapProvidersRepository>()
                .AddTransient<IMapsRepository, MapsRepository>()
                .AddTransient<IProvidedMapsRepository, ProvidedMapsRepository>();

        private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            string conncection = configuration.GetConnectionString("DefaultConnection")!;

            return services.AddDbContext<DayzMapLoaderContext>(options => options
            .EnableSensitiveDataLogging().UseSqlServer(conncection));
        }
    }
}