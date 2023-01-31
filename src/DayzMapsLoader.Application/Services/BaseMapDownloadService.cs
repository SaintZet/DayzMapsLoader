using DayzMapsLoader.Application.Abstractions.Infrastructure;
using DayzMapsLoader.Application.Abstractions.Services;
using DayzMapsLoader.Application.Enums;
using DayzMapsLoader.Application.Managers;
using DayzMapsLoader.Application.Wrappers;
using DayzMapsLoader.Domain.Entities;
using System.Drawing.Imaging;

namespace DayzMapsLoader.Application.Services;

internal class BaseMapDownloadService : IBaseMapDownloadService
{
    protected readonly IProvidedMapsRepository _providedMapsRepository;

    protected readonly MapMergeManager _imageMergeManager;
    protected readonly ExternalApiManager _externalApiManager;

    private int _qualityMultiplier = 25;

    public BaseMapDownloadService(IProvidedMapsRepository providedMapsRepository)
    {
        _providedMapsRepository = providedMapsRepository;

        _imageMergeManager = new MapMergeManager(new MapSize(256), _qualityMultiplier);
        _externalApiManager = new ExternalApiManager();
    }

    public int QualityMultiplier
    {
        get => _qualityMultiplier;
        set
        {
            _qualityMultiplier = value;
            _imageMergeManager.DpiImprovementPercent = value;
        }
    }

    protected MemoryStream GetMapInMemoryStream(ProvidedMap map, int zoom)
    {
        var mapParts = _externalApiManager.GetMapParts(map, zoom);

        Enum.TryParse(map.ImageExtension, true, out ImageExtension extension);

        var image = _imageMergeManager.Merge(mapParts, extension);

        var memoryStream = new MemoryStream();

        image.Save(memoryStream, ImageFormat.Png);

        return memoryStream;
    }
}