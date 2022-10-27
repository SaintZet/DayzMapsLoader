using DayzMapsLoader.MapProviders;
using DayzMapsLoader.Map;
using DayzMapsLoader.Services;
using System;
using System.Drawing;
using System.Collections.Generic;
using System.Diagnostics;

namespace DayzMapsLoader.ConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ImageDownloader imageDownloader = new(MapProviderName.ginfo);
            string path = imageDownloader.SaveMap(@"D:\", MapName.livonia, MapType.topographic, 0);
            Console.WriteLine(path);
            Console.ReadLine();
        }

        private static void Test1(ImageDownloader imageDownloader)
        {
            Bitmap chernorus = imageDownloader.DownloadMap(MapName.chernorus, MapType.topographic, 0);
            Console.WriteLine($"Width: {chernorus.Width} Height: {chernorus.Height}");
        }

        private static void Test2(ImageDownloader imageDownloader)
        {
            string pathToFile = imageDownloader.SaveMap(@"D:\", MapName.livonia, MapType.topographic, 1);
            Console.WriteLine(pathToFile);
        }

        private static void Test3(ImageDownloader imageDownloader)
        {
            imageDownloader.SaveMap(@"D:\", MapName.livonia, MapType.satellite, 1);
        }

        private static void Test4(ImageDownloader imageDownloader)
        {
            var sw = Stopwatch.StartNew();

            List<Bitmap> maps = imageDownloader.DownloadAllMaps(MapType.topographic, 2);

            Console.WriteLine($"Time: {sw.Elapsed.TotalSeconds}");
        }

        private static void Test5(ImageDownloader imageDownloader)
        {
            List<string> maps = imageDownloader.SaveAllMaps(@"D:\", MapType.topographic, 1);

            foreach (var item in maps)
            {
                Console.WriteLine(item);
            }
        }
    }
}