using DayzMapsLoader.Infrastructure.Enums;

namespace DayzMapsLoader.Infrastructure.Entities
{
    public class ProvidersMapAsset
    {
        public int Id { get; set; }
        public MapProvider MapProvider { get; set; }
        public DayzMap DayzMap { get; set; }
        public string MapName { get; set; }
        public MapType MapType { get; set; }
        public int MaxMapLevel { get; set; }
        public string MapVersion { get; set; }
        public ImageExtension MapExtension { get; set; }
    }
}