using RequestsHub.Domain.Contracts;

namespace RequestsHub.Domain.DataTypes.Maps;

internal class Chernorus : IMap
{
#pragma warning disable CS8618

    public Chernorus()
    {
    }

#pragma warning restore CS8618
    public Dictionary<int, MapSize> KeyValuePairsSize { get; set; }
    public MapName MapName => MapName.chernorus;
    public List<TypeMap> TypesMap { get; set; }
    public string Version { get; set; }
    public string MapNameForProvider { get; set; }
    public ImageExtension MapExtension { get; set; }
    public bool IsFirstQuadrant { get; set; }
}