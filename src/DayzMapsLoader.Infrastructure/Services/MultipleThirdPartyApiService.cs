using DayzMapsLoader.Core.Contracts.Infrastructure.Services;
using DayzMapsLoader.Core.Models;

using DayzMapsLoader.Domain.Entities;

using SmartFormat;

namespace DayzMapsLoader.Infrastructure.Services;

public class MultipleThirdPartyApiService : IMultipleThirdPartyApiService
{
    private readonly HttpClient _httpClient = new();

    public async Task<MapParts> GetMapPartsAsync(ProvidedMap map, int zoom)
    {
        var mapSize = MapSize.ConvertZoomLevelRatioSize(zoom);
        var mapParts = new MapParts(mapSize);

        var queryTemplate = map.MapProvider.UrlQueryTemplate;

        var yReversed = mapSize.Width - 1;
        for (var y = 0; y < mapSize.Width; y++)
        {
            for (var x = 0; x < mapSize.Height; x++)
            {
                var parameters = new
                {
                    Map = map,
                    Zoom = zoom,
                    X = x,
                    Y = map.IsFirstQuadrant ? yReversed : y
                };

                var query = Smart.Format(queryTemplate, parameters);

                var response = await _httpClient.GetAsync(query);
                var data = await response.Content.ReadAsByteArrayAsync();

                mapParts.AddPart(x, y, new MapPart(data));
            }

            yReversed--;
        }

        return mapParts;
    }
}