using RequestsHub.Domain.Contracts;

namespace RequestsHub.Domain.Maps
{
    internal class Livonia : IMap
    {
        public Livonia(int height, int width, string version)
        {
            Height = height;
            Width = width;
            Version = version;
        }

        public Livonia()
        {
        }

        public NameMap Name => NameMap.livonia;
        public TypeMap Type { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public string Version { get; set; } = null!;
    }
}