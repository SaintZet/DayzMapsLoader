using RequestsHub.Domain.Contracts;

namespace RequestsHub.Domain.Maps
{
    internal class Namalsk : IMap
    {
        public Namalsk(int height, int width, string version)
        {
            Height = height;
            Width = width;
            Version = version;
        }

        public Namalsk()
        {
        }

        public NameMap Name => NameMap.namalsk;
        public TypeMap Type { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public string Version { get; set; } = null!;
    }
}