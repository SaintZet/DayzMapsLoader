using RequestsHub.Domain.DataTypes.Maps;

namespace RequestsHub.Domain.Contracts;

internal abstract class IMapBuilder
{
    public abstract Chernorus InitializeChernorus();

    public abstract Livonia InitializeLivonia();

    public abstract Banov InitializeBanov();

    public abstract Esseker InitializeEsseker();

    public abstract Namalsk InitializeNamalsk();

    public abstract Takistan InitializeTakistan();
}