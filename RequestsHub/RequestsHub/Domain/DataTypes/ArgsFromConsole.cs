using RequestsHub.Domain.Services;
using RequestsHub.Domain.MapsProviders;
using RequestsHub.Domain.Contracts;

namespace RequestsHub.Domain.DataTypes
{
    public struct ArgsFromConsole
    {
        public MapsProvider NameProvider { get; set; }
        public NameMap NameMap { get; set; }
        public string PathToSave { get; set; }
        public TypeMap TypeMap { get; set; }
        public int Zoom { get; set; }
    }
}