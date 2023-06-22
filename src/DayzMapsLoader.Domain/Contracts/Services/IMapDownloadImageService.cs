namespace DayzMapsLoader.Domain.Contracts.Services;

public interface IMapDownloadImageService
{
    public Task<byte[]> DownloadMapImageAsync(int providerId, int mapId, int typeId, int zoom);

    public Task<byte[,][]> DownloadMapImageInPartsAsync(int providerId, int mapId, int typeId, int zoom);

    public Task<IEnumerable<byte[]>> DownloadAllMapImagesAsync(int providerId, int zoom);
}