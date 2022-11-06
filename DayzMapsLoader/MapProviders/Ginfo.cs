using DayzMapsLoader.Map;
using DayzMapsLoader.MapProviders.Helpers;

namespace DayzMapsLoader.MapProviders;

internal class Ginfo : BaseMapProvider
{
    private readonly List<MapType> _defaultTypesMap = new() { MapType.satellite, MapType.topographic };
    private readonly Dictionary<int, MapSize> _defaultZoomRatioToSize = CalculateHelper.ZoomLevelRatioToSize(7);

    public Ginfo()
    {
        //https://maps.izurvive.com/maps/ChernarusPlus-Top/1.0.0/tiles/8/246/255.webp
        MapInfo chernorus = new(MapName.chernorus, "ChernarusPlus", CalculateHelper.ZoomLevelRatioToSize(8), MapExtension.webp, _defaultTypesMap, "1.0.0");

        //https://maps.izurvive.com/maps/Livonia-Top/1.17/tiles/7/127/0.png
        MapInfo livonia = new(MapName.livonia, "Livonia", _defaultZoomRatioToSize, MapExtension.webp, _defaultTypesMap, "1.19.0");

        //https://maps.izurvive.com/maps/Banov-Top/1.4.1/tiles/4/12/11.webp
        MapInfo banov = new(MapName.banov, "Banov", _defaultZoomRatioToSize, MapExtension.webp, _defaultTypesMap, "1.4.1");

        //https://maps.izurvive.com/maps/Esseker-Top/1.1.0/tiles/4/7/7.png
        MapInfo esseker = new(MapName.esseker, "Esseker", _defaultZoomRatioToSize, MapExtension.png, _defaultTypesMap, "1.1.0", true);

        //https://maps.izurvive.com/maps/Namalsk-Top/0.1.0/tiles/7/0/0.png
        MapInfo namalsk = new(MapName.namalsk, "Namalsk", _defaultZoomRatioToSize, MapExtension.png, _defaultTypesMap, "0.1.0", true);

        //https://maps.izurvive.com/maps/TakistanPlus-Top/1.1.0/tiles/7/0/0.png
        MapInfo takistan = new(MapName.takistan, "TakistanPlus", _defaultZoomRatioToSize, MapExtension.png, _defaultTypesMap, "1.1.0", true);

        Maps = new List<MapInfo> { chernorus, livonia, banov, esseker, namalsk, takistan };
    }

    public override List<MapInfo> Maps { get; }
    public override MapProviderName Name { get => MapProviderName.ginfo; }
}