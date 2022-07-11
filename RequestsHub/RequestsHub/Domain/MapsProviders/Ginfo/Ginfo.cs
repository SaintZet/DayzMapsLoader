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
            Namalsk namalsk = Builder.InitializeNamalsk();
            Takistan takistan = Builder.InitializeTakistan();

            Maps = new List<IMap> { livonia, esseker, namalsk, takistan };
        }

        public List<IMap> Maps { get; }
        public MapProvider Name { get => MapProvider.ginfo; }
        public IMapBuilder Builder { get; }
    }
}