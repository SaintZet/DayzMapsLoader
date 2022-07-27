﻿namespace RequestsHub.Domain.DataTypes.Maps;

internal class Banov : AbstractMap
{
    public Banov(Dictionary<int, MapSize> keyValue, ImageExtension ext, string name, List<MapType> types, string ver, bool IQuad = false)
        : base(keyValue, ext, name, types, ver, IQuad)
    { }

    public override MapName Name => MapName.banov;
}