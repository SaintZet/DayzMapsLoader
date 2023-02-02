namespace DayzMapsLoader.Application.Wrappers;

internal class MapSize
{
    public MapSize(int side)
    {
        Height = side;
        Width = side;
    }

    public MapSize(int height, int width)
    {
        Height = height;
        Width = width;
    }

    public int Height { get; set; }
    public int Width { get; set; }
}