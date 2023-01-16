using DayzMapsLoader.Application.Helpers;
using DayzMapsLoader.Application.Types;
using DayzMapsLoader.Domain.Entities;
using System.Net;

namespace DayzMapsLoader.Application.Managers;

public class ProviderManager
{
    public MapParts GetMapParts(ProvidedMap map, int zoom)
    {
        // TODO: Fix it:
        MapSize mapSize = new(0);  //map.ZoomLevelRatioSize.SingleOrDefault(x => x.Key == zoom).Value;

        MapParts mapParts = new(mapSize);

        WebClient webClient = new();

        var queryBuilder = new QueryBuilder(map, zoom);

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