using DayzMapsLoader.Domain.Entities.Map;
using DayzMapsLoader.Domain.Entities.MapProvider;

namespace DayzMapsLoader.Application.Abstractions.Services
{
    public interface IMapSaver
    {
        public MapProviderName MapProviderName { get; set; }
        public int QualityMultiplier { get; set; }

        public string SaveMap(string pathToSave, MapName mapName, MapType mapType, int mapZoom);

        public string SaveMapInParts(string pathToSave, MapName mapName, MapType mapType, int mapZoom);

        public List<string> SaveAllMaps(string pathToSave, MapType mapType, int mapZoom);
    }
}