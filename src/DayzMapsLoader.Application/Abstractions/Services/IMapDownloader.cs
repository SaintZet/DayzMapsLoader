using DayzMapsLoader.Domain.Entities.Map;
using DayzMapsLoader.Domain.Entities.MapProvider;
using System.Drawing;

namespace DayzMapsLoader.Application.Abstractions.Services
{
    public interface IMapDownloader
    {
        public MapProviderName MapProviderName { get; set; }
        public double QualityImage { get; set; }

        public Bitmap DownloadMap(MapName mapName, MapType mapType, int mapZoom);

        public List<Bitmap> DownloadAllMaps(MapType mapType, int mapZoom);

        public MapParts DownloadMapInParts(MapName mapName, MapType mapType, int mapZoom);

        public List<MapParts> DownloadAllMapsInParts(MapType mapType, int mapZoom);
    }
}