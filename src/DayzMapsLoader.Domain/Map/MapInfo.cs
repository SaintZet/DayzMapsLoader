using DayzMapsLoader.Domain.Common;

namespace DayzMapsLoader.Domain.Map;

public record struct MapInfo(MapName Name,
                            string NameForProvider,
                            Dictionary<int, MapSize> ZoomLevelRatioToSize,
                            MapExtension MapExtension,
                            List<MapType> TypesMap,
                            string Version,
                            bool IsFirstQuadrant = false);