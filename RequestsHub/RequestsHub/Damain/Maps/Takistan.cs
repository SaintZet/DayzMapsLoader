using RequestsHub.Domain.Contracts;

namespace RequestsHub.Domain.Maps
{
    internal class Takistan : IMap
    {
        public Takistan(int height, int width, string version)
        {
            Height = height;
            Width = width;
            Version = version;
        }

        public Takistan()
        {
        }

        public NameMap Name => NameMap.takistan;
        public TypeMap Type { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public string Version { get; set; } = null!;
    }
}