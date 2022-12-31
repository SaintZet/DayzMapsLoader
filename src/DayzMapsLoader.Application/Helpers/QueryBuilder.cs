using DayzMapsLoader.Domain.Entities.Map;
using DayzMapsLoader.Domain.Entities.MapProvider;

namespace DayzMapsLoader.Application.Helpers;

internal class QueryBuilder
{
    private readonly string _queryTemplate;

    public QueryBuilder(MapProviderName mapProvider, MapInfo currentMap, MapType typeMap, int zoom)
    {
        _queryTemplate = BuildTemplateQuery(mapProvider, currentMap, typeMap, zoom);
    }

    public string GetQuery(int i, int j)
    {
        return string.Format(_queryTemplate, i.ToString(), j.ToString());
    }

    private static string BuildTemplateQuery(MapProviderName mapProvider, MapInfo currentMap, MapType typeMap, int zoom)
    {
        return mapProvider switch
        {
            MapProviderName.xam => $"https://static.xam.nu/dayz/maps/{currentMap.NameForProvider}/{currentMap.Version}/{typeMap}/{zoom}/{{0}}/{{1}}.{currentMap.MapExtension}",
            MapProviderName.ginfo => $"https://maps.izurvive.com/maps/{currentMap.NameForProvider}-{GetTypeMap(typeMap)}/{currentMap.Version}/tiles/{zoom}/{{0}}/{{1}}.{currentMap.MapExtension}",
            _ => throw new NotImplementedException("Add new provider in this method!"),
        };
    }

    private static string GetTypeMap(MapType typeMap)
    {
        return typeMap switch
        {
            MapType.topographic => "Top",
            MapType.satellite => "Sat",
            _ => throw new NotImplementedException("Not support this type!"),
        };
    }
}