using DayzMapsLoader.Application.Abstractions.Services;
using DayzMapsLoader.Domain.Entities.Map;
using DayzMapsLoader.Domain.Entities.MapProvider;
using System;
using System.Diagnostics;

namespace DayzMapsLoader.Presentation.ConsoleApp
{
    internal class Application
    {
        private readonly IMapDownloader _mapDownloader;
        private readonly IMapSaver _mapSaver;

        public Application(IMapDownloader mapDownloader, IMapSaver mapSaver, IMapMerger mapMerger)
        {
            _mapSaver = mapSaver;
            _mapSaver.MapProviderName = MapProviderName.ginfo;
            _mapSaver.QualityMultiplier = 100;
        }

        public void StartDoWork()
        {
            var sw = new Stopwatch();
            sw.Start();

            var path = _mapSaver.SaveMap(@"D:\Maps", MapName.livonia, MapType.topographic, 7);

            Console.WriteLine(path);

            sw.Stop();
            Console.WriteLine(sw.Elapsed);

            Console.ReadLine();
        }
    }
}