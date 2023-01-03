using DayzMapsLoader.Domain.Entities.Map;
using DayzMapsLoader.Domain.Entities.MapProvider;
using System.Drawing;

namespace DayzMapsLoader.Application.Abstractions.Services
{
    public interface IMapDownloader
    {
        public MapProviderName MapProviderName { get; set; }
        public int QualityMultiplier { get; set; }

        public Bitmap DownloadMap(MapName mapName, MapType mapType, int mapZoom);

        public IEnumerable<Bitmap> DownloadAllMaps(MapType mapType, int mapZoom);

        public MapParts DownloadMapInParts(MapName mapName, MapType mapType, int mapZoom);

        public IEnumerable<MapParts> DownloadAllMapsInParts(MapType mapType, int mapZoom);
    }
}