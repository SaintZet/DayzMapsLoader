using DayzMapsLoader.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace DayzMapsLoader.Infrastructure.Extensions;

internal static class IncludeDetailsExtensions
{
    public static IQueryable<ProvidedMap>? IncludeDetails(this IQueryable<ProvidedMap>? queryable, bool include = true)
    {
        if (!include)
            return queryable;

        return queryable!
            .Include(p => p.MapProvider)
            .Include(p => p.MapData)
            .Include(p => p.MapType);
    }
}