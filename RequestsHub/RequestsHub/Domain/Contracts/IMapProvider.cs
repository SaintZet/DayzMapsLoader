using RequestsHub.Domain.DataTypes;

namespace RequestsHub.Domain.Contracts
{
    internal interface IMapProvider
    {
        abstract List<IMap> Maps { get; }

        abstract IMapBuilder Builder { get; }

        public string QreateQuery(NameMap nameMap, TypeMap typeMap, int zoom);

        //public void GetMap(NameMap nameMap, TypeMap typeMap, int zoom, string pathToSaveImage);
        //public void GetMapInParts(NameMap nameMap, TypeMap typeMap, int zoom, string pathToSaveImages);
        //public void GetAllMaps(TypeMap typeMap, int zoom, string pathToSaveImages);
        //public void GetAllMapsInParts(NameMap nameMap, TypeMap typeMap, int zoom, string pathToSaveImages);
    }
}