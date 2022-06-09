using RequestsHub.Domain.DataTypes.Maps;

namespace RequestsHub.Domain.Contracts
{
    internal interface IMapBuilder
    {
        public Chernorus InitializeChernorus();
        public Livonia InitializeLivonia();
        public Banov InitializeBanov();
        public Esseker InitializeEsseker();
        public Namalsk InitializeNamalsk();
        public Takistan InitializeTakistan();
    }
}