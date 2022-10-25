using DayzMapsLoader.Map;
using DayzMapsLoader.MapProviders.Helpers;

namespace DayzMapsLoader.MapProviders;

internal class Xam : IMapProvider
{
    private readonly List<MapType> _defaultTypesMap = new() { MapType.satellite, MapType.topographic };
    private readonly Dictionary<int, MapSize> _defaultZoomRatioToSize = CalculateHelper.ZoomLevelRatioToSize(7);

    public Xam()
    {
        //https://static.xam.nu/dayz/maps/chernarusplus/1.17-1/topographic/0/0/0.jpg
        MapInfo chernorus = new(MapName.chernorus, "chernarusplus", _defaultZoomRatioToSize, ImageExtension.jpg, _defaultTypesMap, "1.17-1");

        //https://static.xam.nu/dayz/maps/livonia/1.17-1/topographic/7/127/127.jpg
        MapInfo livonia = new(MapName.livonia, "livonia", _defaultZoomRatioToSize, ImageExtension.jpg, _defaultTypesMap, "1.17-1");

        //https://static.xam.nu/dayz/maps/banov/04.04/topographic/7/127/127.jpg
        MapInfo banov = new(MapName.banov, "banov", _defaultZoomRatioToSize, ImageExtension.jpg, _defaultTypesMap, "04.04");

        //https://static.xam.nu/dayz/maps/esseker/0.58/topographic/0/0/0.jpg
        MapInfo esseker = new(MapName.esseker, "esseker", _defaultZoomRatioToSize, ImageExtension.jpg, _defaultTypesMap, "0.58");

        //https://static.xam.nu/dayz/maps/namalsk/04.19/topographic/7/127/127.jpg
        MapInfo namalsk = new(MapName.namalsk, "namalsk", _defaultZoomRatioToSize, ImageExtension.jpg, _defaultTypesMap, "04.19");

        //https://static.xam.nu/dayz/maps/takistanplus/1.041/topographic/7/127/127.jpg
        MapInfo takistan = new(MapName.takistan, "takistanplus", _defaultZoomRatioToSize, ImageExtension.jpg, _defaultTypesMap, "1.041");

        Maps = new List<MapInfo> { chernorus, livonia, banov, esseker, namalsk, takistan };
    }

    public List<MapInfo> Maps { get; }

    public MapProviderName Name { get => MapProviderName.xam; }

    public override string ToString() => Enum.GetName(Name.GetType(), Name)!;
}