using RequestsHub.Domain.Contracts;

namespace RequestsHub.Domain.Maps
{
    internal class Chernorus : IMap
    {
        public Chernorus(int height, int width, string version)
        {
            Height = height;
            Width = width;
            Version = version;
        }

        public Chernorus()
        {
        }

        public int Height { get; set; }
        public NameMap Name => NameMap.chernorus;
        public string Version { get; set; } = null!;
        public int Width { get; set; }
    }
}