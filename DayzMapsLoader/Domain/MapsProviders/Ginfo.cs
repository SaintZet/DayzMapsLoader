using RequestsHub.Domain.Contracts;
using RequestsHub.Domain.MapsProviders.Helpers;

namespace RequestsHub.Domain.MapsProviders;

internal class Ginfo : IMapProvider
{
    private readonly List<MapType> _defaultTypesMap = new() { MapType.satellite, MapType.topographic };
    private readonly Dictionary<int, MapSize> _defaultZoomRatioToSize = CalculateHelper.ZoomLevelRatioToSize(7);

    public Ginfo()
    {
        //https://maps.izurvive.com/maps/ChernarusPlus-Top/1.0.0/tiles/8/246/255.webp
        Map chernorus = new(MapName.chernorus, CalculateHelper.ZoomLevelRatioToSize(8), ImageExtension.webp, "ChernarusPlus", _defaultTypesMap, "1.0.0");

        //https://maps.izurvive.com/maps/Livonia-Top/1.17/tiles/7/127/0.png
        Map livonia = new(MapName.livonia, _defaultZoomRatioToSize, ImageExtension.png, "Livonia", _defaultTypesMap, "1.17", true);

        //https://maps.izurvive.com/maps/Banov-Top/1.4.1/tiles/4/12/11.webp
        Map banov = new(MapName.banov, _defaultZoomRatioToSize, ImageExtension.webp, "Banov", _defaultTypesMap, "1.4.1");

        //https://maps.izurvive.com/maps/Esseker-Top/1.1.0/tiles/4/7/7.png
        Map esseker = new(MapName.esseker, _defaultZoomRatioToSize, ImageExtension.png, "Esseker", _defaultTypesMap, "1.1.0", true);

        //https://maps.izurvive.com/maps/Namalsk-Top/0.1.0/tiles/7/0/0.png
        Map namalsk = new(MapName.namalsk, _defaultZoomRatioToSize, ImageExtension.png, "Namalsk", _defaultTypesMap, "0.1.0", true);

        //https://maps.izurvive.com/maps/TakistanPlus-Top/1.1.0/tiles/7/0/0.png
        Map takistan = new(MapName.takistan, _defaultZoomRatioToSize, ImageExtension.png, "TakistanPlus", _defaultTypesMap, "1.1.0", true);

        Maps = new HashSet<IMap>() { livonia, esseker, namalsk, takistan };
    }

    public ISet<IMap> Maps { get; }
    public MapProvider Name { get => MapProvider.ginfo; }

    public override string ToString() => Enum.GetName(Name.GetType(), Name)!;
}