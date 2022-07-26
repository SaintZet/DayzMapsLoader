namespace RequestsHub.Domain.DataTypes.Maps;

internal class Namalsk : AbstractMap
{
    public Namalsk(Dictionary<int, MapSize> keyValue, ImageExtension ext, string name, List<TypeMap> types, string ver, bool IQuad = false)
        : base(keyValue, ext, name, types, ver, IQuad)
    { }

    public override MapName Name => MapName.namalsk;
}