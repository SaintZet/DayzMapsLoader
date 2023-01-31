namespace DayzMapsLoader.Application.Abstractions.Services
{
    public interface IMapSaveService
    {
        public int QualityMultiplier { get; set; }

        public string SaveMap(string pathToSave, int providerId, int mapID, int typeId, int zoom);

        public string SaveMapInParts(string pathToSave, int providerId, int mapID, int typeId, int zoom);

        public IEnumerable<string> SaveAllMaps(string pathToSave, int providerId, int typeId, int zoom);
    }
}