namespace RequestsHub.Domain.DataTypes;

public enum Direction
{
    horizontal,
    vertical,
}

public enum MapProvider
{
    xam,
    ginfo,
}

public enum MapName
{
    chernorus,
    livonia,
    namalsk,
    esseker,
    takistan,
    banov
}

public enum MapType
{
    topographic,
    satellite,
    tourist
}

public enum ImageExtension
{
    png,
    jpg,
    webp,
}

public enum CommandType
{
    GetAllMaps,
    GetAllMapsInParts,
    GetMap,
    GetMapInParts,
    MergePartsMap,
    MergePartsAllMaps,
}