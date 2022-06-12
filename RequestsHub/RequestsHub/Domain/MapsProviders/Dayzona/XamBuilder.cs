using RequestsHub.Domain.Contracts;
using RequestsHub.Domain.DataTypes;
using RequestsHub.Domain.DataTypes.Maps;

namespace RequestsHub.Domain.MapsProviders
{
    internal class XamBuilder : IMapBuilder
    {
        public override Chernorus InitializeChernorus()
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

            List<TypeMap> typesMap = new List<TypeMap> { TypeMap.satellite, TypeMap.topographic };

            return new Chernorus
            {
                KeyValuePairsSize = KeyValuePairsSize,
                TypesMap = typesMap,
                Version = "1.17-1",
            };
        }

        public override Livonia InitializeLivonia()
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

            List<TypeMap> typesMap = new List<TypeMap> { TypeMap.satellite, TypeMap.topographic };

            return new Livonia
            {
                KeyValuePairsSize = KeyValuePairsSize,
                TypesMap = typesMap,
                Version = "1.17-1",
            };
        }

        public override Banov InitializeBanov()
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

            List<TypeMap> typesMap = new List<TypeMap> { TypeMap.satellite, TypeMap.topographic };

            return new Banov
            {
                KeyValuePairsSize = KeyValuePairsSize,
                TypesMap = typesMap,
                Version = "04.04",
            };
        }

        public override Esseker InitializeEsseker()
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

            List<TypeMap> typesMap = new List<TypeMap> { TypeMap.satellite, TypeMap.topographic };

            return new Esseker
            {
                KeyValuePairsSize = KeyValuePairsSize,
                TypesMap = typesMap,
                Version = "0.58",
            };
        }

        public override Namalsk InitializeNamalsk()
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

            List<TypeMap> typesMap = new List<TypeMap> { TypeMap.satellite, TypeMap.topographic };

            return new Namalsk
            {
                KeyValuePairsSize = KeyValuePairsSize,
                TypesMap = typesMap,
                Version = "04.19",
            };
        }

        public override Takistan InitializeTakistan()
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

            List<TypeMap> typesMap = new List<TypeMap> { TypeMap.satellite, TypeMap.topographic };

            return new Takistan
            {
                KeyValuePairsSize = KeyValuePairsSize,
                TypesMap = typesMap,
                Version = "1.041",
            };
        }
    }
}