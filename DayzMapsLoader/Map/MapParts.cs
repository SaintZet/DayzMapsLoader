namespace DayzMapsLoader.Map;

internal class MapParts
{
    private readonly MapPart[,] _images;

    public MapParts(MapSize size)
    {
        _images = new MapPart[size.Width, size.Height];
    }

    public int Weight => _images.GetLength(0);
    public int Height => _images.GetLength(1);

    public MapPart GetPartOfMap(int x, int y)
    {
        return _images[x, y];
    }

    public void AddPart(int x, int y, MapPart img)
    {
        _images[x, y] = img;
    }
}