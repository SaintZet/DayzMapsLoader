namespace RequestsHub.Domain.DataTypes.Maps;

internal class Takistan : AbstractMap
{
    public Takistan(Dictionary<int, MapSize> keyValue, ImageExtension ext, string name, List<TypeMap> types, string ver, bool IQuad = false)
        : base(keyValue, ext, name, types, ver, IQuad)
    { }

    public override MapName MapName => MapName.takistan;
}