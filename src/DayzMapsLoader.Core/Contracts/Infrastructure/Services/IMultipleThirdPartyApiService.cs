using DayzMapsLoader.Core.Models;
using DayzMapsLoader.Domain.Entities;

namespace DayzMapsLoader.Core.Contracts.Infrastructure.Services;

public interface IMultipleThirdPartyApiService
{
    public IAsyncEnumerable<MapPart> GetMapPartsAsync(ProvidedMap map, int zoom);
}