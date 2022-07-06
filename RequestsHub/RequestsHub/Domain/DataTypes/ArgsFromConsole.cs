using RequestsHub.Domain.Services;
using RequestsHub.Domain.MapsProviders;
using RequestsHub.Domain.Contracts;

namespace RequestsHub.Domain.DataTypes
{
    public struct ArgsFromConsole
    {
        public MapProvider NameProvider { get; set; }
        public MapName NameMap { get; set; }
        public string PathToSave { get; set; }
        public TypeMap TypeMap { get; set; }
        public int Zoom { get; set; }
    }
}