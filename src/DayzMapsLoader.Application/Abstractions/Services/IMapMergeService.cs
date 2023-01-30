using DayzMapsLoader.Application.Enums;

namespace DayzMapsLoader.Application.Abstractions.Services
{
    public interface IMapMergeService
    {
        public int QualityMultiplier { get; set; }

        public string MergeMapParts(string pathToFolder, ImageExtension extension);
    }
}