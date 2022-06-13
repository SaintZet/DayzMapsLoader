using RequestsHub.Domain.DataTypes;
using RequestsHub.Domain.DataTypes.Maps;
using RequestsHub.Domain.Services.PathsService;

namespace RequestsHub.Domain.Contracts
{
    internal abstract class IMapBuilder
    {
        public abstract Chernorus InitializeChernorus();

        public abstract Livonia InitializeLivonia();

        public abstract Banov InitializeBanov();

        public abstract Esseker InitializeEsseker();

        public abstract Namalsk InitializeNamalsk();

        public abstract Takistan InitializeTakistan();

        public PathsService InitializePaths(IMap map, TypeMap typeMap, int zoom, string pathToSaveImage)
        {
            if (pathToSaveImage == null)
            {
                pathToSaveImage = Directory.GetCurrentDirectory();
            }

            //TODO: check access to write in folder
            if (!Directory.Exists(pathToSaveImage))
            {
                throw new ArgumentException("Bad path!");
            }

            return new PathsService(pathToSaveImage, map.Name.ToString(), typeMap.ToString(), zoom.ToString());
        }
    }
}