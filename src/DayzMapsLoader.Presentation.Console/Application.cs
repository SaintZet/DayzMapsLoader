using DayzMapsLoader.Application.Abstractions;
using DayzMapsLoader.Domain.Entities.Map;
using DayzMapsLoader.Domain.Entities.MapProvider;
using System;

namespace DayzMapsLoader.Presentation.ConsoleApp
{
    internal class Application
    {
        private readonly IMapDownloader _mapDownloader;

        public Application(IMapDownloader mapDownloader)
        {
            _mapDownloader = mapDownloader;

            _mapDownloader.MapProviderName = MapProviderName.ginfo;
            _mapDownloader.QualityImage = 0.5;
        }

        public void StartDoWork()
        {
            string path = _mapDownloader.SaveMap(@"D:\Maps", MapName.banov, MapType.topographic, 2);

            Console.WriteLine(path);

            Console.ReadLine();
        }

        //private static void Test1(MapDownloader imageDownloader)
        //{
        //    Bitmap chernorus = imageDownloader.DownloadMap(MapName.chernorus, MapType.topographic, 0);
        //    Console.WriteLine($"Width: {chernorus.Width} Height: {chernorus.Height}");
        //}

        //private static void Test2(MapDownloader imageDownloader)
        //{
        //    string pathToFile = imageDownloader.SaveMap(@"D:\", MapName.livonia, MapType.topographic, 1);
        //    Console.WriteLine(pathToFile);
        //}

        //private static void Test3(MapDownloader imageDownloader)
        //{
        //    imageDownloader.SaveMap(@"D:\", MapName.livonia, MapType.satellite, 1);
        //}

        //private static void Test4(MapDownloader imageDownloader)
        //{
        //    var sw = Stopwatch.StartNew();

        //    List<Bitmap> maps = imageDownloader.DownloadAllMaps(MapType.topographic, 2);

        //    Console.WriteLine($"Time: {sw.Elapsed.TotalSeconds}");
        //}

        //private static void Test5(MapDownloader imageDownloader)
        //{
        //    List<string> maps = imageDownloader.SaveAllMaps(@"D:\", MapType.topographic, 1);

        //    foreach (var item in maps)
        //    {
        //        Console.WriteLine(item);
        //    }
        //}
    }
}