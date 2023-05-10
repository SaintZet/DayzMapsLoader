using DayzMapsLoader.Domain.Entities;
using DayzMapsLoader.Shared.Wrappers;

namespace DayzMapsLoader.Core.Contracts.Infrastructure.Services;

public interface IMultipleThirdPartyApiService
{
    public Task<MapParts> GetMapPartsAsync(ProvidedMap map, int zoom);
}