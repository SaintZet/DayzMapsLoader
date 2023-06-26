using DayzMapsLoader.Presentation.Wpf.ViewModels.MultipleDownload;
using DayzMapsLoader.Presentation.Wpf.ViewModels.SingleDownload;
using DayzMapsLoader.Presentation.Wpf.Views.MultipleDownload;
using DayzMapsLoader.Presentation.Wpf.Views.SingleDownload;

using Microsoft.Extensions.DependencyInjection;

namespace DayzMapsLoader.Presentation.Wpf.Extensions
{
    public static class AppExtensions
    {
        public static IServiceCollection AddSingleDownloadPageGroup(this IServiceCollection services)
        {
            services.AddTransient<SingleDownloadProvidersViewModel>();
            services.AddTransient<SingleDownloadProvidersPage>();

            services.AddTransient<SingleDownloadMapsViewModel>();
            services.AddTransient<SingleDownloadMapsPage>();

            services.AddTransient<SingleDownloadMapDetailViewModel>();
            services.AddTransient<SingleDownloadMapDetailPage>();
            
            return services;
        }
        
        public static IServiceCollection AddMultipleDownloadPageGroup(this IServiceCollection services)
        {
            services.AddTransient<MultipleDownloadProvidersViewModel>();
            services.AddTransient<MultipleDownloadProvidersPage>();
        
            services.AddTransient<MultipleDownloadMapDetailViewModel>();
            services.AddTransient<MultipleDownloadMapDetailPage>();
            
            return services;
        }
    }
}