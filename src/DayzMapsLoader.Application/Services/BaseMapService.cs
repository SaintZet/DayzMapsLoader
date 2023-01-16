//using DayzMapsLoader.Application.Abstractions.Infrastructure;
//using DayzMapsLoader.Application.Managers;
//using DayzMapsLoader.Application.Types;

//namespace DayzMapsLoader.Application.Services;

//public class BaseMapService
//{
//    protected readonly ProviderManager _providerManager = new();
//    protected readonly ImageMerger _imageMerger = new(new MapSize(256), 25);
//    protected readonly IMapsDbContext _mapsDbContext;

//    internal BaseMapService(IMapsDbContext mapsDbContext)
//    {
//        _mapsDbContext = mapsDbContext;
//    }

//    public int QualityMultiplier
//    {
//        get => _imageMerger.DpiImprovementPercent;
//        set => _imageMerger.DpiImprovementPercent = value;
//    }
//}