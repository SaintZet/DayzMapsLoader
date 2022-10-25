using DayzMapsLoader.Contracts;
using DayzMapsLoader.DataTypes;
using DayzMapsLoader.Entities;
using DayzMapsLoader.Entities.MapsProviders.Helpers;

namespace RequestsHub.Domain.MapsProviders;

internal class Xam : IMapProvider
{
    private readonly List<MapType> _defaultTypesMap = new() { MapType.satellite, MapType.topographic };
    private readonly Dictionary<int, MapSize> _defaultZoomRatioToSize = CalculateHelper.ZoomLevelRatioToSize(7);

    public Xam()
    {
        //https://static.xam.nu/dayz/maps/chernarusplus/1.17-1/topographic/0/0/0.jpg
        Map chernorus = new(MapName.chernorus, _defaultZoomRatioToSize, ImageExtension.jpg, "chernarusplus", _defaultTypesMap, "1.17-1");

        //https://static.xam.nu/dayz/maps/livonia/1.17-1/topographic/7/127/127.jpg
        Map livonia = new(MapName.livonia, _defaultZoomRatioToSize, ImageExtension.jpg, "livonia", _defaultTypesMap, "1.17-1");

        //https://static.xam.nu/dayz/maps/banov/04.04/topographic/7/127/127.jpg
        Map banov = new(MapName.banov, _defaultZoomRatioToSize, ImageExtension.jpg, "banov", _defaultTypesMap, "04.04");

        //https://static.xam.nu/dayz/maps/esseker/0.58/topographic/0/0/0.jpg
        Map esseker = new(MapName.esseker, _defaultZoomRatioToSize, ImageExtension.jpg, "esseker", _defaultTypesMap, "0.58");

        //https://static.xam.nu/dayz/maps/namalsk/04.19/topographic/7/127/127.jpg
        Map namalsk = new(MapName.namalsk, _defaultZoomRatioToSize, ImageExtension.jpg, "namalsk", _defaultTypesMap, "04.19");

        //https://static.xam.nu/dayz/maps/takistanplus/1.041/topographic/7/127/127.jpg
        Map takistan = new(MapName.takistan, _defaultZoomRatioToSize, ImageExtension.jpg, "takistanplus", _defaultTypesMap, "1.041");

        Maps = new List<IMap> { chernorus, livonia, banov, esseker, namalsk, takistan };
    }

    public List<IMap> Maps { get; }

    public MapProvider Name { get => MapProvider.xam; }

    public override string ToString() => Enum.GetName(Name.GetType(), Name)!;
}