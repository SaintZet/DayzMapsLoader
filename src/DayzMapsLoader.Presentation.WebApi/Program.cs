namespace DayzMapsLoader.Presentation.WebApi;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var pathConfigPath = Path.Combine(Directory.GetCurrentDirectory(), "Properties");
        var configuration = new ConfigurationBuilder()
            .SetBasePath(pathConfigPath)
            .AddJsonFile("appsettings.json")
            .Build();

        await CreateHostBuilder(args, configuration).Build().RunAsync();
    }

    private static IHostBuilder CreateHostBuilder(string[] args, IConfiguration configuration)
        => Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
                webBuilder.UseConfiguration(configuration);
            });
}