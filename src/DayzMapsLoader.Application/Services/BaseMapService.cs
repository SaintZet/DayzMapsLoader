using DayzMapsLoader.Application.Abstractions;
using DayzMapsLoader.Application.Managers;
using DayzMapsLoader.Application.Managers.MergerSquareImages;
using DayzMapsLoader.Domain.Entities.MapProvider;

namespace DayzMapsLoader.Application.Services
{
    public class BaseMapService
    {
        protected readonly ProviderManager _providerManager = new();
        protected readonly MergerSquareImages _mergerSquareImages = new(0.5);

        private MapProviderName _mapProviderName;

        private readonly IMapsDbContext _mapsDbContext;

        internal BaseMapService(IMapsDbContext mapsDbContext)
        {
            _mapsDbContext = mapsDbContext;
        }

        public MapProviderName MapProviderName
        {
            get => _mapProviderName;
            set
            {
                _providerManager.MapProviderEntity = _mapsDbContext.GetMapProvider(value);
                _mapProviderName = value;
            }
        }
        public double QualityImage
        {
            get => _mergerSquareImages.DpiImprovementPercent;
            set => _mergerSquareImages.DpiImprovementPercent = value;
        }
    }
}