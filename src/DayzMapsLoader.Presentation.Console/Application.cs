using DayzMapsLoader.Application.Abstractions.Services;
using DayzMapsLoader.Domain.Entities.Map;
using DayzMapsLoader.Domain.Entities.MapProvider;
using System;

namespace DayzMapsLoader.Presentation.ConsoleApp
{
    internal class Application
    {
        private readonly IMapDownloader _mapDownloader;
        private readonly IMapSaver _mapSaver;

        public Application(IMapDownloader mapDownloader, IMapSaver mapSaver, IMapMerger mapMerger)
        {
            _mapSaver = mapSaver;
            _mapSaver.MapProviderName = MapProviderName.xam;
            _mapSaver.QualityImage = 0.5;
        }

        public void StartDoWork()
        {
            var path = _mapSaver.SaveMapInParts(@"D:\Maps", MapName.chernorus, MapType.satellite, 2);

            Console.WriteLine(path);

            Console.ReadLine();
        }
    }
}