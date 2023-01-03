namespace DayzMapsLoader.Domain.Entities.Map;

public class Size
{
    public Size(int side)
    {
        Height = side;
        Width = side;
    }

    public Size(int height, int width)
    {
        Height = height;
        Width = width;
    }

    public int Height { get; set; }
    public int Width { get; set; }
}