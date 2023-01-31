namespace DayzMapsLoader.Application.Abstractions.Services;

internal interface IMapDownloadImageService
{
    public Task<byte[]> DownloadMapImageAsync(int providerId, int mapID, int typeId, int zoom);

    public Task<byte[,][]> DownloadMapImageInPartsAsync(int providerId, int mapID, int typeId, int zoom);

    public Task<IEnumerable<byte[]>> DownloadAllMapImages(int providerId, int zoom);
}