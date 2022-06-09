using RequestsHub.Domain.Contracts;
using RequestsHub.Domain.DataTypes;
using RequestsHub.Domain.DataTypes.Maps;

namespace RequestsHub.Domain.MapsProviders
{
    internal class GinfoBuilder : IMapBuilder
    {
        public Chernorus InitializeChernorus()
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

        public Livonia InitializeLivonia()
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

        public Esseker InitializeEsseker()
        {
            throw new NotImplementedException();
        }

        public Banov InitializeBanov()
        {
            throw new NotImplementedException();
        }

        public Namalsk InitializeNamalsk()
        {
            throw new NotImplementedException();
        }

        public Takistan InitializeTakistan()
        {
            throw new NotImplementedException();
        }
    }
}
