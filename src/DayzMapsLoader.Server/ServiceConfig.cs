using DayzMapsLoader.Application.Abstractions.Infrastructure;
using DayzMapsLoader.Infrastructure.DbContexts;
using MediatR;

namespace DayzMapsLoader.Server;

public static class ServiceConfig
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddScoped<IMapsDbContext, JsonMapsDbContext>();
        services.AddMediatR(typeof(Program));

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }
}