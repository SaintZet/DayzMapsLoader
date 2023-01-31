using DayzMapsLoader.Application.Helpers;
using DayzMapsLoader.Application.Types;
using DayzMapsLoader.Domain.Entities;
using System.Net;

namespace DayzMapsLoader.Application.Managers;

public class ExternalApiManager
{
    public MapParts GetMapParts(ProvidedMap map, int zoom)
    {
        MapSize mapSize = ConvertZoomLevelRatioSize(zoom);

        MapParts mapParts = new(mapSize);

        WebClient webClient = new();

        QueryBuilder queryBuilder = new(map, zoom);

        int yReversed = mapSize.Width - 1;
        for (int y = 0; y < mapSize.Width; y++)
        {
            for (int x = 0; x < mapSize.Height; x++)
            {
                string query = map.IsFirstQuadrant ? queryBuilder.BuildQuery(x, yReversed) : queryBuilder.BuildQuery(x, y);

                mapParts.AddPart(x, y, new MapPart(webClient.DownloadData(query)));
            }

            yReversed--;
        }

        return mapParts;
    }

    private static MapSize ConvertZoomLevelRatioSize(int zoomLevel)
    {
        int height = 1, weight = 1;

        for (int i = 0; i < zoomLevel; i++)
        {
            height *= 2;
            weight *= 2;
        }

        return new MapSize(height, weight);
    }
}