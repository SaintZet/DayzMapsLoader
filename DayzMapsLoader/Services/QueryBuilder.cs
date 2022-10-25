using DayzMapsLoader.Map;
using DayzMapsLoader.MapProviders;

namespace DayzMapsLoader.Services;

internal class QueryBuilder
{
    private readonly string _queryTemplate;
    private readonly string _mapName;
    private readonly string _version;
    private readonly string _type;
    private readonly string _zoom;
    private readonly string _extension;
    private readonly IMapProvider _mapProvider;

    public QueryBuilder(IMapProvider mapProvider, MapInfo currentMap, MapType typeMap, int zoom)
    {
        _mapProvider = mapProvider;
        _zoom = zoom.ToString();

        _mapName = currentMap.NameForProvider;
        _version = currentMap.Version;
        _extension = currentMap.MapExtension.ToString();
        _type = GetTypeMap(typeMap)!;
        _queryTemplate = BuildQueryTemplate();
    }

    public string BuildQueryTemplate()
    {
        switch (_mapProvider.Name)
        {
            case MapProviderName.xam:
                return @"https://static.xam.nu/dayz/maps/{0}/{1}/{2}/{3}/{4}/{5}.{6}";

            case MapProviderName.ginfo:
                return @"https://maps.izurvive.com/maps/{0}-{1}/{2}/tiles/{3}/{4}/{5}.{6}";
        }
        throw new NotImplementedException("RequestsHub.Domain.Services.QueryBuilder.BuildQueryBase: Add new provider in this method!");
    }

    internal string GetQuery(int i, int j)
    {
        switch (_mapProvider.Name)
        {
            case MapProviderName.xam:
                return string.Format(_queryTemplate, _mapName, _version, _type, _zoom, i.ToString(), j.ToString(), _extension);

            case MapProviderName.ginfo:
                return string.Format(_queryTemplate, _mapName, _type, _version, _zoom, i.ToString(), j.ToString(), _extension);
        }
        throw new NotImplementedException("RequestsHub.Domain.Services.QueryBuilder.GetQuery: Add new provider in this method!");
    }

    private string? GetTypeMap(MapType typeMap)
    {
        switch (_mapProvider.Name)
        {
            case MapProviderName.xam:
                return typeMap.ToString(); ;

            case MapProviderName.ginfo:
                switch (typeMap)
                {
                    case MapType.topographic:
                        return "Top";

                    case MapType.satellite:
                        return "Sat";

                    case MapType.tourist:
                    default:
                        throw new NotImplementedException("RequestsHub.Domain.Services.QueryBuilder.GetTypeMap: Not support this type!");
                }
        }
        throw new NotImplementedException("RequestsHub.Domain.Services.QueryBuilder.GetTypeMap: Add new provider in this method!");
    }
}