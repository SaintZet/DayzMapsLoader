using DayzMapsLoader.Application.Helpers;
using DayzMapsLoader.Domain.Entities.Map;
using DayzMapsLoader.Domain.Entities.MapProvider;
using System.Net;

namespace DayzMapsLoader.Application.Managers;

public class ProviderManager
{
    public MapProvider MapProvider { get; set; }

    public override string ToString() => Enum.GetName(MapProvider.Name.GetType(), MapProvider.Name)!;

    public MapInfo GetMapInfo(MapName mapName, MapType mapType, int mapZoom)
    {
        //TODO: Add validation.
        return MapProvider.Maps.SingleOrDefault(x => x.Name == mapName);
    }

    public MapParts GetMapParts(MapInfo map, MapType mapType, int mapZoom)
    {
        Size mapSize = map.ZoomLevelRatioSize.SingleOrDefault(x => x.Key == mapZoom).Value;

        MapParts mapParts = new(mapSize);

        WebClient webClient = new();

        var queryBuilder = new QueryBuilder(MapProvider.Name, map, mapType, mapZoom);

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