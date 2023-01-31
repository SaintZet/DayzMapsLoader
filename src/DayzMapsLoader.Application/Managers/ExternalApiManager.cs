using DayzMapsLoader.Application.Helpers;
using DayzMapsLoader.Application.Wrappers;
using DayzMapsLoader.Domain.Entities;

namespace DayzMapsLoader.Application.Managers;

internal class ExternalApiManager
{
    private readonly HttpClient _httpClient = new();

    public async Task<MapParts> GetMapPartsAsync(ProvidedMap map, int zoom)
    {
        MapSize mapSize = ConvertZoomLevelRatioSize(zoom);
        MapParts mapParts = new(mapSize);

        QueryBuilder queryBuilder = new(map, zoom);

        int yReversed = mapSize.Width - 1;
        for (int y = 0; y < mapSize.Width; y++)
        {
            for (int x = 0; x < mapSize.Height; x++)
            {
                string query = map.IsFirstQuadrant ? queryBuilder.BuildQuery(x, yReversed) : queryBuilder.BuildQuery(x, y);

                var response = await _httpClient.GetAsync(query);
                var data = await response.Content.ReadAsByteArrayAsync();

                mapParts.AddPart(x, y, new MapPart(data));
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