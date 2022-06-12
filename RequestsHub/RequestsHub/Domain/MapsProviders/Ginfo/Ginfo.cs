using RequestsHub.Domain.Contracts;
using RequestsHub.Domain.DataTypes;
using RequestsHub.Domain.DataTypes.Maps;

namespace RequestsHub.Domain.MapsProviders
{
    internal class Ginfo : IMapProvider
    {
        public Ginfo()
        {
            Builder = new GinfoBuilder();

            Chernorus chernorus = Builder.InitializeChernorus();
            Livonia livonia = Builder.InitializeLivonia();
            Banov banov = Builder.InitializeBanov();
            Esseker esseker = Builder.InitializeEsseker();
            //Namalsk namalsk = Builder.InitializeNamalsk();
            //Takistan takistan = Builder.InitializeTakistan();

            Maps = new List<IMap> { chernorus, livonia, banov, esseker };
        }

        public List<IMap> Maps { get; }

        public IMapBuilder Builder { get; set; }

        public string QreateQuery(NameMap nameMap, TypeMap typeMap, int zoom)
        {
            throw new NotImplementedException();
        }
    }
}