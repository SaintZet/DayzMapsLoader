namespace DayzMapsLoader.Shared.Wrappers;

public class MapParts
{
    private readonly MapPart[,] _images;
    private readonly byte[,][] _rawImages;

    public MapParts(MapSize size)
    {
        _images = new MapPart[size.WidthPixels, size.HeightPixels];
        _rawImages = new byte[size.WidthPixels, size.HeightPixels][];
    }

    public int Weight => _images.GetLength(0);
    public int Height => _images.GetLength(1);

    public byte[,][] GetRawMapParts()
    {
        return _rawImages;
    }

    public MapPart GetPartOfMap(int x, int y)
    {
        return _images[x, y];
    }

    public void AddPart(int x, int y, MapPart img)
    {
        _images[x, y] = img;
        _rawImages[x, y] = img.Data;
    }
}