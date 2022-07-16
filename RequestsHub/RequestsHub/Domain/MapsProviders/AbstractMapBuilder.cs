using RequestsHub.Domain.DataTypes.Maps;

namespace RequestsHub.Domain.MapsProviders;

internal abstract class AbstractMapBuilder
{
    public abstract Chernorus InitializeChernorus();

    public abstract Livonia InitializeLivonia();

    public abstract Banov InitializeBanov();

    public abstract Esseker InitializeEsseker();

    public abstract Namalsk InitializeNamalsk();

    public abstract Takistan InitializeTakistan();
}