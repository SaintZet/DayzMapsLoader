using RequestsHub.Domain.MapsProviders;

namespace RequestsHub.Domain.Contracts;

internal interface IMapProvider
{
    abstract List<IMap> Maps { get; }
    abstract MapProvider Name { get; }
    abstract AbstractMapBuilder Builder { get; }
}