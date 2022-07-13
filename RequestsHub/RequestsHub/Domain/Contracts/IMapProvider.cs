namespace RequestsHub.Domain.Contracts;

internal interface IMapProvider
{
    abstract List<IMap> Maps { get; }
    abstract MapProvider Name { get; }
    abstract IMapBuilder Builder { get; }

    //public void GetMap(NameMap nameMap, TypeMap typeMap, int zoom, string pathToSaveImage);
    //public void GetMapInParts(NameMap nameMap, TypeMap typeMap, int zoom, string pathToSaveImages);
    //public void GetAllMaps(TypeMap typeMap, int zoom, string pathToSaveImages);
    //public void GetAllMapsInParts(NameMap nameMap, TypeMap typeMap, int zoom, string pathToSaveImages);
}