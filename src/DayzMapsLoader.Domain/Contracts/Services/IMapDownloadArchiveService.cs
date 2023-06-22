namespace DayzMapsLoader.Domain.Contracts.Services;

public interface IMapDownloadArchiveService
{
    public Task<(byte[] data, string name)> DownloadMapImageArchiveAsync(int providerId, int mapId, int typeId, int zoom);

    public Task<(byte[] data, string name)> DownloadMapImagePartsArchiveAsync(int providerId, int mapId, int typeId, int zoom);

    public Task<(byte[] data, string name)> DownloadAllMapsImagesArchiveAsync(int providerId, int zoom);
}