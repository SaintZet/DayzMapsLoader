using DayzMapsLoader.Presentation.WebApi.Extensions;
using DayzMapsLoader.DependencyInjection;

using MediatR;

namespace DayzMapsLoader.Presentation.WebApi;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.ConfigureApplication();
        services.AddControllers();
        services.AddSwagger();
        services.AddCors();
        services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
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