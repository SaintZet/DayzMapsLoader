using System.IO;
using System.Windows;
using System.Windows.Threading;

using DayzMapsLoader.Core.Extensions;
using DayzMapsLoader.Infrastructure.Extensions;
using DayzMapsLoader.Presentation.Wpf.Contracts.Services;
using DayzMapsLoader.Presentation.Wpf.Contracts.Views;
using DayzMapsLoader.Presentation.Wpf.Extensions;
using DayzMapsLoader.Presentation.Wpf.Models;
using DayzMapsLoader.Presentation.Wpf.Services;
using DayzMapsLoader.Presentation.Wpf.ViewModels;
using DayzMapsLoader.Presentation.Wpf.ViewModels.MultipleDownload;
using DayzMapsLoader.Presentation.Wpf.ViewModels.SingleDownload;
using DayzMapsLoader.Presentation.Wpf.Views;
using DayzMapsLoader.Presentation.Wpf.Views.MultipleDownload;
using DayzMapsLoader.Presentation.Wpf.Views.SingleDownload;
using MediatR;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DayzMapsLoader.Presentation.Wpf;

public partial class App
{
    private IHost _host;

    public T GetService<T>()
            where T : class
        => _host.Services.GetService(typeof(T)) as T;

    private static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
    {
        // App Host
        services.AddHostedService<ApplicationHostService>();

        // Core Services
        services.AddCoreLayer();
        services.AddInfrastructureLayer(context.Configuration.GetConnectionString("DefaultConnection")!);
        services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

        // Services
        services.AddSingleton<IApplicationInfoService, ApplicationInfoService>();
        services.AddSingleton<ISystemService, SystemService>();
        services.AddSingleton<IPersistAndRestoreService, PersistAndRestoreService>();
        services.AddSingleton<IThemeSelectorService, ThemeSelectorService>();
        services.AddSingleton<IDownloadArchiveService, DownloadArchiveService>();
        services.AddSingleton<IPageService, PageService>();
        services.AddSingleton<INavigationService, NavigationService>();

        // Views and ViewModels
        services.AddTransient<IShellWindow, ShellWindow>();
        services.AddTransient<ShellViewModel>();

        services.AddTransient<SettingsViewModel>();
        services.AddTransient<SettingsPage>();

        services.AddSingleDownloadPageGroup();
        services.AddMultipleDownloadPageGroup();

        // Configuration
        services.Configure<AppConfig>(context.Configuration.GetSection(nameof(AppConfig)));
    }

    private async void OnStartup(object sender, StartupEventArgs e)
    {
        var appLocation = Directory.GetCurrentDirectory();
        var configPath = Path.Combine(appLocation!, "Properties", "appsettings.json");

        _host = Host.CreateDefaultBuilder(e.Args)
            .ConfigureAppConfiguration((_, config) =>
                config.AddJsonFile(configPath, optional: false, reloadOnChange: true))
            .ConfigureServices(ConfigureServices)
            .Build();

        await _host.StartAsync();
    }

    private async void OnExit(object sender, ExitEventArgs e)
    {
        await _host.StopAsync();
        _host.Dispose();
        _host = null;
    }

    private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
    {
        // TODO: Please log and handle the exception as appropriate to your scenario For more info
        // see https://docs.microsoft.com/dotnet/api/system.windows.application.dispatcherunhandledexception?view=netcore-3.0
    }
}