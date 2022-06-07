using RequestsHub.Domain.Contracts;

namespace RequestsHub.Domain.Maps
{
    internal class Banov : IMap
    {
        public Banov(int height, int width, string version)
        {
            Height = height;
            Width = width;
            Version = version;
        }

        public Banov()
        {
        }

        public NameMap Name => NameMap.banov;
        public TypeMap Type { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public string Version { get; set; } = null!;
    }
}