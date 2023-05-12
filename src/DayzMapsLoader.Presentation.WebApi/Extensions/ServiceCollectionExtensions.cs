using Microsoft.OpenApi.Models;

using System.Reflection;

namespace DayzMapsLoader.Presentation.WebApi.Extensions;

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
}