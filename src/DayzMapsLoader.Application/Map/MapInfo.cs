namespace DayzMapsLoader.Application.Map;

internal record struct MapInfo(MapName Name,
                               string NameForProvider,
                               Dictionary<int, MapSize> ZoomLevelRatioToSize,
                               MapExtension MapExtension,
                               List<MapType> TypesMap,
                               string Version,
                               bool IsFirstQuadrant = false);