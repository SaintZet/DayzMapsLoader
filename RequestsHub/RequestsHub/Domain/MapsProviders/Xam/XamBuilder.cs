using RequestsHub.Domain.DataTypes.Maps;

namespace RequestsHub.Domain.MapsProviders;

internal class XamBuilder : AbstractMapBuilder
{
    //https://static.xam.nu/dayz/maps/chernarusplus/1.17-1/topographic/0/0/0.jpg
    public override Chernorus InitializeChernorus()
    {
        Dictionary<int, MapSize> KeyValuePairsSize = new()
        {
            {0, new MapSize(1, 1)},
            {1, new MapSize(2, 2)},
            {2, new MapSize(4, 4)},
            {3, new MapSize(8, 8)},
            {4, new MapSize(16, 16)},
            {5, new MapSize(32, 32)},
            {6, new MapSize(64, 64)},
            {7, new MapSize(128, 128)},
        };

        List<MapType> typesMap = new List<MapType> { MapType.satellite, MapType.topographic };

        return new Chernorus(KeyValuePairsSize, ImageExtension.jpg, "chernarusplus", typesMap, "1.17-1");
    }

    //https://static.xam.nu/dayz/maps/livonia/1.17-1/topographic/7/127/127.jpg
    public override Livonia InitializeLivonia()
    {
        Dictionary<int, MapSize> KeyValuePairsSize = new()
        {
            {0, new MapSize(1, 1)},
            {1, new MapSize(2, 2)},
            {2, new MapSize(4, 4)},
            {3, new MapSize(8, 8)},
            {4, new MapSize(16, 16)},
            {5, new MapSize(32, 32)},
            {6, new MapSize(64, 64)},
            {7, new MapSize(128, 128)},
        };

        List<MapType> typesMap = new List<MapType> { MapType.satellite, MapType.topographic };

        return new Livonia(KeyValuePairsSize, ImageExtension.jpg, "livonia", typesMap, "1.17-1");
    }

    //https://static.xam.nu/dayz/maps/banov/04.04/topographic/7/127/127.jpg
    public override Banov InitializeBanov()
    {
        Dictionary<int, MapSize> KeyValuePairsSize = new()
        {
            {0, new MapSize(1, 1)},
            {1, new MapSize(2, 2)},
            {2, new MapSize(4, 4)},
            {3, new MapSize(8, 8)},
            {4, new MapSize(16, 16)},
            {5, new MapSize(32, 32)},
            {6, new MapSize(64, 64)},
            {7, new MapSize(128, 128)},
        };

        List<MapType> typesMap = new List<MapType> { MapType.satellite, MapType.topographic };

        return new Banov(KeyValuePairsSize, ImageExtension.jpg, "banov", typesMap, "04.04");
    }

    //https://static.xam.nu/dayz/maps/esseker/0.58/topographic/0/0/0.jpg
    public override Esseker InitializeEsseker()
    {
        Dictionary<int, MapSize> KeyValuePairsSize = new()
        {
            {0, new MapSize(1, 1)},
            {1, new MapSize(2, 2)},
            {2, new MapSize(4, 4)},
            {3, new MapSize(8, 8)},
            {4, new MapSize(16, 16)},
            {5, new MapSize(32, 32)},
            {6, new MapSize(64, 64)},
            {7, new MapSize(128, 128)},
        };

        List<MapType> typesMap = new List<MapType> { MapType.satellite, MapType.topographic };

        return new Esseker(KeyValuePairsSize, ImageExtension.jpg, "esseker", typesMap, "0.58");
    }

    //https://static.xam.nu/dayz/maps/namalsk/04.19/topographic/7/127/127.jpg
    public override Namalsk InitializeNamalsk()
    {
        Dictionary<int, MapSize> KeyValuePairsSize = new()
        {
            {0, new MapSize(1, 1)},
            {1, new MapSize(2, 2)},
            {2, new MapSize(4, 4)},
            {3, new MapSize(8, 8)},
            {4, new MapSize(16, 16)},
            {5, new MapSize(32, 32)},
            {6, new MapSize(64, 64)},
            {7, new MapSize(128, 128)},
        };

        List<MapType> typesMap = new List<MapType> { MapType.satellite, MapType.topographic };

        return new Namalsk(KeyValuePairsSize, ImageExtension.jpg, "namalsk", typesMap, "04.19");
    }

    //https://static.xam.nu/dayz/maps/takistanplus/1.041/topographic/7/127/127.jpg
    public override Takistan InitializeTakistan()
    {
        Dictionary<int, MapSize> KeyValuePairsSize = new()
        {
            {0, new MapSize(1, 1)},
            {1, new MapSize(2, 2)},
            {2, new MapSize(4, 4)},
            {3, new MapSize(8, 8)},
            {4, new MapSize(16, 16)},
            {5, new MapSize(32, 32)},
            {6, new MapSize(64, 64)},
            {7, new MapSize(128, 128)},
        };

        List<MapType> typesMap = new List<MapType> { MapType.satellite, MapType.topographic };

        return new Takistan(KeyValuePairsSize, ImageExtension.jpg, "takistanplus", typesMap, "1.041");
    }
}