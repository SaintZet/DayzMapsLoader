using DayzMapsLoader.Application.Enums;

namespace DayzMapsLoader.Application.Abstractions.Services
{
    public interface IMapMerger
    {
        public int QualityMultiplier { get; set; }

        public string MergeMapParts(string pathToFolder, ImageExtension extension);
    }
}