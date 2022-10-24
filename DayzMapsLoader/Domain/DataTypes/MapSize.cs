namespace RequestsHub.Domain.DataTypes;

internal class MapSize
{
    public MapSize(int height, int width)
    {
        Height = height;
        Width = width;
    }

    public int Height { get; set; }
    public int Width { get; set; }
}