using DayzMapsLoader.Application.Abstractions.Infrastructure;
using DayzMapsLoader.Application.Abstractions.Services;
using DayzMapsLoader.Application.Services;
using DayzMapsLoader.Infrastructure.Contexts;
using DayzMapsLoader.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace DayzMapsLoader.Presentation.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "DayzMapLoader API",
                    Description = "An ASP.NET Core Web API for managing DayzMapLoader.",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Example Contact",
                        Url = new Uri("https://example.com/contact")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Example License",
                        Url = new Uri("https://example.com/license")
                    }
                });

                // using System.Reflection;
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

            builder.Services.AddDbContext<DayzMapLoaderContext>(options => options
                .EnableSensitiveDataLogging()
                .UseSqlServer(@"Server =.; DataBase=DayzMapLoader; User id=sa; password=Micr0!nvest; Integrated Security=True; TrustServerCertificate=True;"));

            builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddTransient<IMapProvidersRepository, MapProvidersRepository>();
            builder.Services.AddTransient<IMapsRepository, MapsRepository>();
            builder.Services.AddTransient<IProvidedMapsRepository, ProvidedMapsRepository>();

            builder.Services.AddTransient<IMapDownloader, MapDownloader>();

            builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "DayzMapsLoader API V1");
                    options.InjectStylesheet("/swagger-ui/theme-material.css");
                });
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}