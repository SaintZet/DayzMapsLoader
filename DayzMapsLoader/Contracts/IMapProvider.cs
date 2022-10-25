using DayzMapsLoader.DataTypes;

namespace DayzMapsLoader.Contracts;

internal interface IMapProvider
{
    abstract List<IMap> Maps { get; }
    abstract MapProvider Name { get; }

    string ToString();
}