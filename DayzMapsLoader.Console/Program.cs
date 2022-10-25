using DayzMapsLoader.MapProviders;
using DayzMapsLoader.Map;
using DayzMapsLoader.Services;
using System;
using System.Drawing;

namespace DayzMapsLoader.ConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ImageDownloader imageDownloader = new(MapProviderName.xam);

            Bitmap chernorus = imageDownloader.DownloadMap(MapName.chernorus, MapType.topographic, 7);
            Bitmap livonia = imageDownloader.DownloadMap(MapName.livonia, MapType.topographic, 7);

            Console.ReadLine();
        }
    }
}