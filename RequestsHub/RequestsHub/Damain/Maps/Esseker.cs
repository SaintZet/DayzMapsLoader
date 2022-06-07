using RequestsHub.Domain.Contracts;

namespace RequestsHub.Domain.Maps
{
    internal class Esseker : IMap
    {
        public Esseker(int height, int width, string version)
        {
            Height = height;
            Width = width;
            Version = version;
        }

        public Esseker()
        {
        }

        public NameMap Name => NameMap.esseker;
        public TypeMap Type { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public string Version { get; set; } = null!;
    }
}