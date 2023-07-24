using DayzMapsLoader.Core.Contracts.Infrastructure.Services;
using DayzMapsLoader.Core.Models;
using DayzMapsLoader.Domain.Entities;
using SmartFormat;

namespace DayzMapsLoader.Infrastructure.Services;

public class MultipleThirdPartyApiService : IMultipleThirdPartyApiService
{
    private readonly HttpClient _httpClient = new();

    public async IAsyncEnumerable<MapPart> GetMapPartsAsync(ProvidedMap map, int zoom)
    {
        var mapSize = MapSize.ConvertZoomLevelRatioSize(zoom);
        var queryTemplate = map.MapProvider.UrlQueryTemplate;

        var yReversed = mapSize.Width - 1;
        var loaders = new List<Func<Task<MapPart>>>(mapSize.Width * mapSize.Height);
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
					return new MapPart(queryX, queryY, data);
				});
            }

            yReversed--;
        }

        var concurrentLimit = Math.Max(4, zoom * zoom);
        while (loaders.Count != 0)
        {
	        var parallelRequests = loaders.Take(concurrentLimit).Select(x => x.Invoke()).ToList();
	        var tasksCount = parallelRequests.Count;
	        while (parallelRequests.Count != 0)
	        {
		        var executedTask = await Task.WhenAny(parallelRequests);
		        yield return executedTask.Result;
		        parallelRequests.Remove(executedTask);
	        }
		        
	        loaders.RemoveRange(0, tasksCount);
        }
    }
}