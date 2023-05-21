using DayzMapsLoader.Core.Extensions;
using DayzMapsLoader.Infrastructure.Extensions;
using DayzMapsLoader.Presentation.WebApi.Extensions;

using MediatR;

namespace DayzMapsLoader.Presentation.WebApi;

public class Startup
{
    private readonly IConfiguration _config;

    public Startup(IConfiguration configuration)
        => _config = configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddCoreLayer();
        services.AddInfrastructureLayer(_config.GetConnectionString("DefaultConnection")!);
        services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
        services.AddControllers();
        services.AddSwagger();
        services.AddCors();
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "DayzMapsLoader API V1");
            options.InjectStylesheet("/swagger-ui/theme-material.css");
        });
        app.UseRouting();
        app.UseStaticFiles();
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.UseCors(builder => builder.AllowAnyOrigin());
        app.UseEndpoints(endpoints => endpoints.MapControllers());
    }
}