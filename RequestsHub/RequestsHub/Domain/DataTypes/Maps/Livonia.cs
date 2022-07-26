namespace RequestsHub.Domain.DataTypes.Maps;

internal class Livonia : AbstractMap
{
    public Livonia(Dictionary<int, MapSize> keyValue, ImageExtension ext, string name, List<TypeMap> types, string ver, bool IQuad = false)
        : base(keyValue, ext, name, types, ver, IQuad)
    { }

    public override MapName Name => MapName.livonia;
}