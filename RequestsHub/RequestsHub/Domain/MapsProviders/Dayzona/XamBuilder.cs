using RequestsHub.Domain.Contracts;
using RequestsHub.Domain.DataTypes;
using RequestsHub.Domain.DataTypes.Maps;

namespace RequestsHub.Domain.MapsProviders
{
    internal class XamBuilder : IMapBuilder
    {
        //https://static.xam.nu/dayz/maps/chernarusplus/1.17-1/topographic/0/0/0.jpg
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
                MapExtension = ImageExtension.jpg,
                MapNameForProvider = "chernarusplus",
                TypesMap = typesMap,
                Version = "1.17-1",
            };
        }

        //https://static.xam.nu/dayz/maps/livonia/1.17-1/topographic/7/127/127.jpg
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
                MapExtension = ImageExtension.jpg,
                MapNameForProvider = "livonia",
                TypesMap = typesMap,
                Version = "1.17-1",
            };
        }

        //https://static.xam.nu/dayz/maps/banov/04.04/topographic/7/127/127.jpg
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
                MapExtension = ImageExtension.jpg,
                MapNameForProvider = "banov",
                TypesMap = typesMap,
                Version = "04.04",
            };
        }

        //https://static.xam.nu/dayz/maps/esseker/0.58/topographic/0/0/0.jpg
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
                MapExtension = ImageExtension.jpg,
                MapNameForProvider = "esseker",
                TypesMap = typesMap,
                Version = "0.58",
            };
        }

        //https://static.xam.nu/dayz/maps/namalsk/04.19/topographic/7/127/127.jpg
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
                MapExtension = ImageExtension.jpg,
                MapNameForProvider = "namalsk",
                TypesMap = typesMap,
                Version = "04.19",
            };
        }

        //https://static.xam.nu/dayz/maps/takistanplus/1.041/topographic/7/127/127.jpg
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
                MapExtension = ImageExtension.jpg,
                MapNameForProvider = "takistanplus",
                TypesMap = typesMap,
                Version = "1.041",
            };
        }
    }
}