using DayzMapsLoader.Core.Contracts.Infrastructure.Services;
using DayzMapsLoader.Core.Models;

using DayzMapsLoader.Domain.Entities;
using Nito.AsyncEx;
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

        var asyncLock = new AsyncLock();
        var yReversed = mapSize.Width - 1;
        var loaders = new List<Func<Task>>(mapSize.Width * mapSize.Height);
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

                var queryY = y;
                var queryX = x;
                loaders.Add(async () =>
				{
					var query = Smart.Format(queryTemplate, parameters);

					var response = await _httpClient.GetAsync(query);
					var data = await response.Content.ReadAsByteArrayAsync();
					using var locked = await asyncLock.LockAsync();
					mapParts.AddPart(queryX, queryY, new MapPart(data));
				});
            }

            yReversed--;
        }

        var concurrentLimit = zoom * zoom;
        while (loaders.Count != 0)
        {
	        var parallelRequests = loaders.Take(concurrentLimit).ToArray();
	        await Task.WhenAll(parallelRequests.Select(x => x.Invoke()));
	        loaders.RemoveRange(0, parallelRequests.Length);
        }

        return mapParts;
    }
}