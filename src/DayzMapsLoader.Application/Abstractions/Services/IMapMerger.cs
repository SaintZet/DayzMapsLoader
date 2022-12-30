using DayzMapsLoader.Domain.Entities.Map;
using DayzMapsLoader.Domain.Entities.MapProvider;

namespace DayzMapsLoader.Application.Abstractions.Services
{
    public interface IMapMerger
    {
        public MapProviderName MapProviderName { get; set; }
        public double QualityImage { get; set; }

        public string MergeMapParts(string pathToFolder, ImageExtension extension);
    }
}