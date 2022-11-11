using DayzMapsLoader.Application.Map;
using DayzMapsLoader.Application.Services;
using System.Net;

namespace DayzMapsLoader.Application.MapProviders;

public enum MapProviderName
{
    xam,
    ginfo,
}

internal abstract class BaseMapProvider
{
    public abstract List<MapInfo> Maps { get; }
    public abstract MapProviderName Name { get; }

    public override string ToString() => Enum.GetName(Name.GetType(), Name)!;

    public MapInfo GetMapInfo(MapName mapName, MapType mapType, int mapZoom)
    {
        Validate.CheckMapAtProvider(this, mapName);

        var map = Maps.SingleOrDefault(x => x.Name == mapName);

        Validate.CheckTypeAtMap(map, mapType);
        Validate.CheckZoomAtMap(map, mapZoom);

        return map;
    }

    public MapParts GetMapParts(MapInfo map, MapType mapType, int mapZoom)
    {
        MapSize mapSize = map.ZoomLevelRatioToSize.SingleOrDefault(x => x.Key == mapZoom).Value;

        MapParts mapParts = new(mapSize);

        WebClient webClient = new();

        var queryBuilder = new QueryBuilder(Name, map, mapType, mapZoom);

        int yReversed = mapSize.Width - 1;
        for (int y = 0; y < mapSize.Width; y++)
        {
            for (int x = 0; x < mapSize.Height; x++)
            {
                string query = map.IsFirstQuadrant ? queryBuilder.GetQuery(x, yReversed) : queryBuilder.GetQuery(x, y);
                mapParts.AddPart(x, y, new MapPart(webClient.DownloadData(query)));
            }
            yReversed--;
        }

        return mapParts;
    }
}