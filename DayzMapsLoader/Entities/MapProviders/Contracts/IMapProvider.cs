using DayzMapsLoader.Map;

namespace DayzMapsLoader.MapProviders;

internal interface IMapProvider
{
    abstract List<MapInfo> Maps { get; }
    abstract MapProviderName Name { get; }

    string ToString();
}