using DayzMapsLoader.Application.Abstractions;
using DayzMapsLoader.Application.Abstractions.Services;
using DayzMapsLoader.Application.Managers;
using DayzMapsLoader.Domain.Entities.Map;
using System.Drawing;

namespace DayzMapsLoader.Application.Services
{
    public class MapMerger : BaseMapService, IMapMerger
    {
        public MapMerger(IMapsDbContext mapsDbContext) : base(mapsDbContext)
        {
        }

        public string MergeMapParts(string pathToFolder, ImageExtension extension)
        {
            var mapParts = GetMapParts(pathToFolder);

            Bitmap image = _mergerSquareImages.Merge(mapParts, extension);

            return ImageSaver.SaveImageToHardDisk(image, pathToFolder, extension);
        }

        private MapParts GetMapParts(string pathToFolder)
        {
            List<string> verticals = GetMapPathOnVericals(pathToFolder);

            var mapSize = new MapSize(verticals.Count);

            var mapParts = new MapParts(mapSize);

            for (int y = 0; y < verticals.Count; y++)
            {
                List<string> horizontals = GetMapPathOnHorizontal(verticals[y]);

                for (int x = 0; x < horizontals.Count; x++)
                {
                    var part = new MapPart(File.ReadAllBytes(horizontals[x]));

                    mapParts.AddPart(x, y, part);
                }
            }

            return mapParts;
        }

        private List<string> GetMapPathOnVericals(string resourcePath) => new DirectoryInfo(resourcePath).GetDirectories()
            .Select(d => d.FullName)
            .OrderBy(s => s.Length)
            .ThenBy(s => s)
            .ToList();

        private List<string> GetMapPathOnHorizontal(string currentDirectoryName) => new DirectoryInfo(currentDirectoryName).GetFiles()
            .Select(s => s.FullName)
            .OrderBy(s => s.Length)
            .ThenBy(s => s)
            .ToList();
    }
}