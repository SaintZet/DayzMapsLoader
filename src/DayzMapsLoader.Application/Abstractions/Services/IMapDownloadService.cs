using System.Drawing;

namespace DayzMapsLoader.Application.Abstractions.Services
{
    public interface IMapDownloadService
    {
        public int QualityMultiplier { get; set; }

        public Task<byte[]> DownloadMapImage(int providerId, int mapID, int typeId, int zoom);

        public Task<byte[,][]> DownloadMapImageInParts(int providerId, int mapID, int typeId, int zoom);

        public Task<(byte[] data, string name)> DownloadMapImageArchive(int providerId, int mapID, int typeId, int zoom);

        public Task<(byte[] data, string name)> DownloadMapImagePartsArchive(int providerId, int mapID, int typeId, int zoom);

        //public IEnumerable<Task<byte[]>> DownloadAllMaps(int providerId, int zoom);

        //public IEnumerable<Task<byte[,][]>> DownloadAllMapsInParts(int providerId, int zoom);
    }
}