namespace DayzMapsLoader.Application.Abstractions.Services;

internal interface IMapDownloadArchiveService : IBaseMapDownloadService
{
    public Task<(byte[] data, string name)> DownloadMapImageArchiveAsync(int providerId, int mapID, int typeId, int zoom);

    public Task<(byte[] data, string name)> DownloadMapImagePartsArchiveAsync(int providerId, int mapID, int typeId, int zoom);

    public Task<(byte[] data, string name)> DownloadAllMapsImagesArchiveAsync(int providerId, int zoom);
}