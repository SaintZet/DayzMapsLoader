using Microsoft.Extensions.DependencyInjection;

namespace DayzMapsLoader.Presentation.ConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var serviceProvider = new Startup().ConfigureServices();

            var app = serviceProvider.GetRequiredService<Application>();

            app.StartDoWork();
        }
    }
}