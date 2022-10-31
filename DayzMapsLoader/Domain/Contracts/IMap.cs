using System.Data;
using FluentValidation;

namespace RequestsHub.Domain.Contracts;

public interface IMap
{
    Dictionary<int, MapSize> ZoomLevelRatioToSize { get; set; }
    ImageExtension MapExtension { get; set; }
    List<MapType> TypesMap { get; }
    MapName Name { get; }
    string MapNameForProvider { get; set; }
    string Version { get; set; }
    bool IsFirstQuadrant { get; }
}

public class MapValidation : AbstractValidator<IMap>
{
    public MapValidation(MapType mapType, int zoom)
    {
        RuleFor(map => map.TypesMap)
            .Must(x => x.Contains(mapType))
            .WithMessage($"MapType {mapType} doesnt exists in Map");
        RuleForEach(map => map.ZoomLevelRatioToSize)
            .Must(x => x.Key == zoom)
            .WithErrorCode($"Zoom {zoom} doesn't exists in Map");
    }
}
