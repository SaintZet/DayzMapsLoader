using DayzMapsLoader.Application.Abstractions;
using DayzMapsLoader.Application.Abstractions.Services;
using DayzMapsLoader.Application.Managers;
using DayzMapsLoader.Domain.Entities.Map;
using System.Drawing;

namespace DayzMapsLoader.Application.Services
{
    public class MapSaver : BaseMapService, IMapSaver
    {
        public MapSaver(IMapsDbContext mapsDbContext) : base(mapsDbContext)
        {
        }

        public ImageExtension ImageExtansionForSave { get; set; } = ImageExtension.jpg;

        public string SaveMap(string pathToSave, MapName mapName, MapType mapType, int mapZoom)
        {
            MapInfo mapInfo = _providerManager.GetMapInfo(mapName, mapType, mapZoom);

            MapParts mapParts = _providerManager.GetMapParts(mapInfo, mapType, mapZoom);

            Bitmap image = _mergerSquareImages.Merge(mapParts, mapInfo.MapExtension);

            string path = $@"{pathToSave}\{_providerManager}\{mapInfo.Name}\{mapType}\{mapInfo.Version}\{mapZoom}";

            return ImageSaver.SaveImageToHardDisk(image, path, ImageExtansionForSave);
        }

        public string SaveMapInParts(string pathToSave, MapName mapName, MapType mapType, int mapZoom)
        {
            MapInfo mapInfo = _providerManager.GetMapInfo(mapName, mapType, mapZoom);

            MapParts mapParts = _providerManager.GetMapParts(mapInfo, mapType, mapZoom);

            string path = $@"{pathToSave}\{_providerManager}\{mapInfo.Name}\{mapType}\{mapInfo.Version}\{mapZoom}";

            return ImageSaver.SaveImageToHardDisk(mapParts, path, ImageExtansionForSave);
        }

        public List<string> SaveAllMaps(string pathToSave, MapType mapType, int mapZoom)
        {
            List<string> result = new();

            Parallel.ForEach(_providerManager.MapProviderEntity.Maps, mapInfo =>
            {
                var path = SaveMap(pathToSave, mapInfo.Name, mapType, mapZoom);

                result.Add(path);
            });

            return result;
        }
    }
}