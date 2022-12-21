using DayzMapsLoader.Domain.Entities.Map;
using DayzMapsLoader.Domain.Entities.MapProvider;
using System.Drawing;

namespace DayzMapsLoader.Application.Abstractions
{
    public interface IMapDownloader
    {
        public MapProviderName MapProviderName { get; set; }
        public double QualityImage { get; set; }

        public Bitmap DownloadMap(MapName mapName, MapType mapType, int mapZoom);

        public List<Bitmap> DownloadAllMaps(MapType mapType, int mapZoom);

        public byte[,][] DownloadMapInParts(MapName mapName, MapType mapType, int mapZoom);

        public List<byte[,][]> DownloadAllMapsInParts(MapType mapType, int mapZoom);

        public string SaveMap(string pathToSave, MapName mapName, MapType mapType, int mapZoom);

        public List<string> SaveAllMaps(string pathToSave, MapType mapType, int mapZoom);
    }
}