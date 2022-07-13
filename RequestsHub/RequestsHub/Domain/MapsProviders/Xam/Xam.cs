using RequestsHub.Domain.Contracts;
using RequestsHub.Domain.DataTypes.Maps;

namespace RequestsHub.Domain.MapsProviders;

internal class Xam : IMapProvider
{
    public Xam()
    {
        Builder = new XamBuilder();

        Chernorus chernorus = Builder.InitializeChernorus();
        Livonia livonia = Builder.InitializeLivonia();

        Banov banov = Builder.InitializeBanov();
        Esseker esseker = Builder.InitializeEsseker();
        Namalsk namalsk = Builder.InitializeNamalsk();
        Takistan takistan = Builder.InitializeTakistan();

        Maps = new List<IMap> { chernorus, livonia, banov, esseker, namalsk, takistan };
    }

    public List<IMap> Maps { get; }

    public AbstractMapBuilder Builder { get; }

    public MapProvider Name { get => MapProvider.xam; }
}