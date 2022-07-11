using RequestsHub.Domain.Contracts;
using RequestsHub.Domain.DataTypes;
using RequestsHub.Domain.DataTypes.Maps;

namespace RequestsHub.Domain.MapsProviders
{
    internal class GinfoBuilder : IMapBuilder
    {
        //https://maps.izurvive.com/maps/ChernarusPlus-Top/1.0.0/tiles/8/246/255.webp
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
            KeyValuePairsSize.Add(8, new MapSize(256, 256));

            List<TypeMap> typesMap = new List<TypeMap> { TypeMap.satellite, TypeMap.topographic };

            return new Chernorus
            {
                KeyValuePairsSize = KeyValuePairsSize,
                MapExtension = ImageExtension.webp,
                MapNameForProvider = "ChernarusPlus",
                TypesMap = typesMap,
                Version = "1.0.0",
            };
        }

        //https://maps.izurvive.com/maps/Livonia-Top/1.17/tiles/7/127/0.png
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
                MapExtension = ImageExtension.png,
                MapNameForProvider = "Livonia",
                TypesMap = typesMap,
                Version = "1.17",
                IsFirstQuadrant = true,
            };
        }

        //https://maps.izurvive.com/maps/Banov-Top/1.4.1/tiles/4/12/11.webp
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
                MapExtension = ImageExtension.webp,
                MapNameForProvider = "Banov",
                TypesMap = typesMap,
                Version = "1.4.1",
            };
        }

        //https://maps.izurvive.com/maps/Esseker-Top/1.1.0/tiles/4/7/7.png
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
                MapExtension = ImageExtension.png,
                MapNameForProvider = "Esseker",
                TypesMap = typesMap,
                Version = "1.1.0",
                IsFirstQuadrant = true,
            };
        }

        //https://maps.izurvive.com/maps/Namalsk-Top/0.1.0/tiles/7/0/0.png
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
                MapExtension = ImageExtension.png,
                MapNameForProvider = "Namalsk",
                TypesMap = typesMap,
                Version = "0.1.0",
                IsFirstQuadrant = true,
            };
        }

        //https://maps.izurvive.com/maps/TakistanPlus-Top/1.1.0/tiles/7/0/0.png
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
                MapExtension = ImageExtension.png,
                MapNameForProvider = "TakistanPlus",
                TypesMap = typesMap,
                Version = "1.1.0",
                IsFirstQuadrant = true,
            };
        }
    }
}