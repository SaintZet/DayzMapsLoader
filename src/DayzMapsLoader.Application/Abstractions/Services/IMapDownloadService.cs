namespace DayzMapsLoader.Application.Abstractions.Services;

public interface IMapDownloadService
{
    public int QualityMultiplier { get; set; }

    public Task<byte[]> DownloadMapImageAsync(int providerId, int mapID, int typeId, int zoom);

    public Task<byte[,][]> DownloadMapImageInPartsAsync(int providerId, int mapID, int typeId, int zoom);

    public Task<(byte[] data, string name)> DownloadMapImageArchiveAsync(int providerId, int mapID, int typeId, int zoom);

    public Task<(byte[] data, string name)> DownloadMapImagePartsArchiveAsync(int providerId, int mapID, int typeId, int zoom);

    public Task<(byte[] data, string name)> DownloadAllMapsImagesArchiveAsync(int providerId, int zoom);
}