using DayzMapsLoader.Core.Models;
using DayzMapsLoader.Domain.Entities;

namespace DayzMapsLoader.Core.Contracts.Infrastructure.Services;

public interface IMultipleThirdPartyApiService
{
    public Task<MapParts> GetMapPartsAsync(ProvidedMap map, int zoom);
}