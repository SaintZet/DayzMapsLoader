using RequestsHub.Domain.Contracts;
using RequestsHub.Domain.DataTypes.Maps;

namespace RequestsHub.Domain.MapsProviders;

internal class GinfoBuilder : IMapBuilder
{
    //https://maps.izurvive.com/maps/ChernarusPlus-Top/1.0.0/tiles/8/246/255.webp
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
            {8, new MapSize(256, 256)},
        };

        List<TypeMap> typesMap = new List<TypeMap> { TypeMap.satellite, TypeMap.topographic };

        return new Chernorus(KeyValuePairsSize, ImageExtension.webp, "ChernarusPlus", typesMap, "1.0.0");
    }

    //https://maps.izurvive.com/maps/Livonia-Top/1.17/tiles/7/127/0.png
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

        List<TypeMap> typesMap = new List<TypeMap> { TypeMap.satellite, TypeMap.topographic };

        return new Livonia(KeyValuePairsSize, ImageExtension.png, "Livonia", typesMap, "1.17", true);
    }

    //https://maps.izurvive.com/maps/Banov-Top/1.4.1/tiles/4/12/11.webp
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

        List<TypeMap> typesMap = new List<TypeMap> { TypeMap.satellite, TypeMap.topographic };

        return new Banov(KeyValuePairsSize, ImageExtension.webp, "Banov", typesMap, "1.4.1");
    }

    //https://maps.izurvive.com/maps/Esseker-Top/1.1.0/tiles/4/7/7.png
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

        List<TypeMap> typesMap = new List<TypeMap> { TypeMap.satellite, TypeMap.topographic };

        return new Esseker(KeyValuePairsSize, ImageExtension.png, "Esseker", typesMap, "1.1.0", true);
    }

    //https://maps.izurvive.com/maps/Namalsk-Top/0.1.0/tiles/7/0/0.png
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

        List<TypeMap> typesMap = new List<TypeMap> { TypeMap.satellite, TypeMap.topographic };

        return new Namalsk(KeyValuePairsSize, ImageExtension.png, "Namalsk", typesMap, "0.1.0", true);
    }

    //https://maps.izurvive.com/maps/TakistanPlus-Top/1.1.0/tiles/7/0/0.png
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

        List<TypeMap> typesMap = new List<TypeMap> { TypeMap.satellite, TypeMap.topographic };

        return new Takistan(KeyValuePairsSize, ImageExtension.png, "TakistanPlus", typesMap, "1.1.0", true);
    }
}