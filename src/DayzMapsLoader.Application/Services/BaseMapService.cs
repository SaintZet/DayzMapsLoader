using DayzMapsLoader.Application.Abstractions.Infrastructure;
using DayzMapsLoader.Application.Managers;
using DayzMapsLoader.Domain.Entities.Map;
using DayzMapsLoader.Domain.Entities.MapProvider;

namespace DayzMapsLoader.Application.Services;

public class BaseMapService
{
    protected readonly ProviderManager _providerManager = new();
    protected readonly ImageMerger _imageMerger = new(new MapSize(256), 25);

    private readonly IMapsDbContext _mapsDbContext;

    private MapProviderName _mapProviderName;

    internal BaseMapService(IMapsDbContext mapsDbContext)
    {
        _mapsDbContext = mapsDbContext;
        _providerManager.MapProvider = _mapsDbContext.GetMapProvider(_mapProviderName);
    }

    public MapProviderName MapProviderName
    {
        get => _mapProviderName;
        set
        {
            if (_mapProviderName == value)
                return;

            _mapProviderName = value;
            _providerManager.MapProvider = _mapsDbContext.GetMapProvider(value);
        }
    }
    public int QualityMultiplier
    {
        get => _imageMerger.DpiImprovementPercent;
        set => _imageMerger.DpiImprovementPercent = value;
    }
}