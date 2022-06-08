using RequestsHub.Domain.Contracts;
using RequestsHub.Domain.DataTypes;
using RequestsHub.Domain.Maps;

namespace RequestsHub.Domain.MapsProviders
{
    internal class Dayzona : IMapProvider
    {
        private List<IMap> _mapProvider;

        public Dayzona()
        {
            Chernorus chernorus = new Chernorus
            {
                Version = "1.18",
            };

            Livonia livonia = new Livonia
            {
                Version = "1.18",
            };

            _mapProvider = new List<IMap> { chernorus, livonia };
        }

        public List<IMap> Maps => _mapProvider;

        public string QueryBuilder(NameMap nameMap, TypeMap typeMap, int zoom)
        {
            throw new NotImplementedException();
        }

        public void SaveImage(NameMap nameMap, TypeMap typeMap, int zoom, string pathToSaveImage)
        {
            throw new NotImplementedException();
        }

        public void SaveImages(NameMap nameMap, TypeMap typeMap, int zoom, string pathToSaveImages)
        {
            throw new NotImplementedException();
        }
    }
}