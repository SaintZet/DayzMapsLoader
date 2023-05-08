using DayzMapsLoader.Application.Abstractions.Infrastructure.Services;
using DayzMapsLoader.Domain.Entities;
using DayzMapsLoader.Shared.Wrappers;
using SmartFormat;

namespace DayzMapsLoader.Infrastructure.Services;

public class MultipleThirdPartyApiService : IMultipleThirdPartyApiService
{
    private readonly HttpClient _httpClient = new();

    public async Task<MapParts> GetMapPartsAsync(ProvidedMap map, int zoom)
    {
        MapSize mapSize = MapSize.ConvertZoomLevelRatioSize(zoom);
        MapParts mapParts = new(mapSize);

        string queryTemplate = map.MapProvider.UrlQueryTemplate;

        int yReversed = mapSize.Width - 1;
        for (int y = 0; y < mapSize.Width; y++)
        {
            for (int x = 0; x < mapSize.Height; x++)
            {
                var parameters = new
                {
                    Map = map,
                    Zoom = zoom,
                    X = x,
                    Y = map.IsFirstQuadrant ? yReversed : y
                };

                string query = Smart.Format(queryTemplate, parameters);

                var response = await _httpClient.GetAsync(query);
                var data = await response.Content.ReadAsByteArrayAsync();

                mapParts.AddPart(x, y, new MapPart(data));
            }

            yReversed--;
        }

        return mapParts;
    }
}