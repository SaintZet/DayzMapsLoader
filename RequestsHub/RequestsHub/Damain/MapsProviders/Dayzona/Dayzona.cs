using RequestsHub.Domain.Contracts;
using RequestsHub.Domain.DataTypes;

namespace RequestsHub.Domain.MapsProviders
{
    internal class Dayzona : IMapProvider
    {
        private List<IMap> _mapProvider;

        public Dayzona()
        {
            _mapProvider = new List<IMap> { };
        }

        public List<IMap> Maps => throw new NotImplementedException();

        public IMapBuilder Builder => throw new NotImplementedException();

        public void GetAllMaps(TypeMap typeMap, int zoom, string pathToSaveImages)
        {
            throw new NotImplementedException();
        }

        public void GetAllMapsInParts(NameMap nameMap, TypeMap typeMap, int zoom, string pathToSaveImages)
        {
            throw new NotImplementedException();
        }

        public void GetMap(NameMap nameMap, TypeMap typeMap, int zoom, string pathToSaveImage)
        {
            throw new NotImplementedException();
        }

        public void GetMapInParts(NameMap nameMap, TypeMap typeMap, int zoom, string pathToSaveImages)
        {
            throw new NotImplementedException();
        }

        public string QueryBuilder(NameMap nameMap, TypeMap typeMap, int zoom)
        {
            throw new NotImplementedException();
        }
    }
}