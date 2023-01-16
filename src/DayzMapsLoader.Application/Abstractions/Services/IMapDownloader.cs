namespace DayzMapsLoader.Application.Abstractions.Services
{
    public interface IMapDownloader
    {
        public int QualityMultiplier { get; set; }

        public Task<byte[]> DownloadMap(int providerId, int mapID, int typeId, int zoom);

        //public IEnumerable<Task<byte[]>> DownloadAllMaps(int providerId, int zoom);

        public Task<byte[,][]> DownloadMapInParts(int providerId, int mapID, int typeId, int zoom);

        //public IEnumerable<Task<byte[,][]>> DownloadAllMapsInParts(int providerId, int zoom);
    }
}