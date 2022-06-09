using RequestsHub.Domain.Contracts;
using RequestsHub.Domain.DataTypes;
using RequestsHub.Domain.DataTypes.Maps;

namespace RequestsHub.Domain.MapsProviders
{
    internal class Ginfo : IMapProvider
    {
        private List<IMap> _mapProvider;

        public Ginfo()
        {
            Builder = new GinfoBuilder();

            Chernorus chernorus = Builder.InitializeChernorus();
            Livonia livonia = Builder.InitializeLivonia();

            //Banov banov = Builder.InitializeBanov();
            //Esseker esseker = Builder.InitializeEsseker();
            //Namalsk namalsk = Builder.InitializeNamalsk();
            //Takistan takistan = Builder.InitializeTakistan();

            _mapProvider = new List<IMap> { chernorus, livonia };
        }

        public List<IMap> Maps => _mapProvider;

        public IMapBuilder Builder { get; set; }

        public string QueryBuilder(NameMap nameMap, TypeMap typeMap, int zoom)
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

        public void GetAllMaps(TypeMap typeMap, int zoom, string pathToSaveImages)
        {
            throw new NotImplementedException();
        }

        public void GetAllMapsInParts(NameMap nameMap, TypeMap typeMap, int zoom, string pathToSaveImages)
        {
            throw new NotImplementedException();
        }
    }
}