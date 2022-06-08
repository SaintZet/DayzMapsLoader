using RequestsHub.Damain.DataTypes;
using RequestsHub.Domain.Contracts;
using RequestsHub.Domain.DataTypes;
using RequestsHub.Domain.Maps;

namespace RequestsHub.Domain.MapsProviders
{
    internal class Ginfo : IMapProvider
    {
        private List<IMap> _mapProvider;

        public Ginfo()
        {
            Banov banov = InitializeBanov();
            Chernorus chernorus = InitializeChernorus();
            Esseker esseker = InitializeEsseker();
            Livonia livonia = InitializeLivonia();

            _mapProvider = new List<IMap> { chernorus, livonia };
        }

        private Esseker InitializeEsseker()
        {
            throw new NotImplementedException();
        }

        private Banov InitializeBanov()
        {
            throw new NotImplementedException();
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

        private Chernorus InitializeChernorus()
        {
            Dictionary<int, MapSize> KeyValuePairsSize = new Dictionary<int, MapSize>();
            KeyValuePairsSize.Add(0, new MapSize(1, 1));
            KeyValuePairsSize.Add(1, new MapSize(2, 2));
            KeyValuePairsSize.Add(2, new MapSize(4, 4));
            KeyValuePairsSize.Add(3, new MapSize(8, 8));
            KeyValuePairsSize.Add(4, new MapSize(16, 16));
            KeyValuePairsSize.Add(5, new MapSize(32, 32));
            KeyValuePairsSize.Add(6, new MapSize(64, 64));
            KeyValuePairsSize.Add(7, new MapSize(128, 128));
            KeyValuePairsSize.Add(8, new MapSize(256, 256));

            List<TypeMap> typesMap = new List<TypeMap> { TypeMap.satellite, TypeMap.topographic, TypeMap.tourist };

            return new Chernorus
            {
                KeyValuePairsSize = KeyValuePairsSize,
                TypesMap = typesMap,
                Version = "1.0.0",
            };
        }

        private Livonia InitializeLivonia()
        {
            Dictionary<int, MapSize> KeyValuePairsSize = new Dictionary<int, MapSize>();
            KeyValuePairsSize.Add(0, new MapSize(1, 1));
            KeyValuePairsSize.Add(1, new MapSize(2, 2));
            KeyValuePairsSize.Add(2, new MapSize(4, 4));
            KeyValuePairsSize.Add(3, new MapSize(8, 8));
            KeyValuePairsSize.Add(4, new MapSize(16, 16));
            KeyValuePairsSize.Add(5, new MapSize(32, 32));
            KeyValuePairsSize.Add(6, new MapSize(64, 64));
            KeyValuePairsSize.Add(7, new MapSize(128, 128));

            List<TypeMap> typesMap = new List<TypeMap> { TypeMap.satellite, TypeMap.topographic, TypeMap.tourist };

            return new Livonia
            {
                KeyValuePairsSize = KeyValuePairsSize,
                TypesMap = typesMap,
                Version = "1.17",
            };
        }
    }
}