using DayzMapsLoader.Domain.Entities.Map;
using DayzMapsLoader.Domain.Entities.MapProvider;
using DayzMapsLoader.Infrastructure.DTO;

namespace DayzMapsLoader.Infrastructure.Converters;

internal static class ConvertDTO
{
    public static MapProvider ToProviderEntity(ProviderDTO providerDTO)
    {
        return new()
        {
            Name = (MapProviderName)providerDTO.Name,
            Maps = ConvertMaps(providerDTO.Maps),
        };
    }

    private static List<MapInfo> ConvertMaps(List<MapInfoDto>? mapInfo)
    {
        var result = new List<MapInfo>();

        foreach (var mapDto in mapInfo!)
        {
            result.Add(new MapInfo
            {
                IsFirstQuadrant = mapDto.IsFirstQuadrant,
                MapExtension = (ImageExtension)mapDto.MapExtension,
                Name = (MapName)mapDto.Name,
                NameForProvider = mapDto.NameForProvider!,
                TypesMap = ConvertMapTypes(mapDto.TypesMap),
                Version = mapDto.Version!,
                ZoomLevelRatioToSize = ConvertZoomLevelRatioSize(mapDto.ZoomLevelRatioToSize)
            });
        }

        return result;
    }

    private static List<MapType> ConvertMapTypes(List<int>? typesMap)
    {
        var result = new List<MapType>();

        typesMap!.ForEach(t => result.Add((MapType)t));

        return result;
    }

    private static Dictionary<int, MapSize> ConvertZoomLevelRatioSize(int zoomLevelRatioToSize)
    {
        int height = 1, weight = 1;

        Dictionary<int, MapSize> zoomLevels = new()
        {
            { 0, new MapSize(height, weight) }
        };

        for (int i = 1; i < zoomLevelRatioToSize; i++)
        {
            height *= 2;
            weight *= 2;
            zoomLevels.Add(i, new MapSize(height, weight));
        }

        return zoomLevels;
    }
}