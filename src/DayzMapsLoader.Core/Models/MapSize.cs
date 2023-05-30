namespace DayzMapsLoader.Core.Models;

public class MapSize
{
    public MapSize(int widthPixels, int heightPixels)
    {
        Width = widthPixels;
        Height = heightPixels;
    }

    public int Width { get; }
    public int Height { get; }

    public static MapSize ConvertZoomLevelRatioSize(int zoomLevel)
    {
        int height = 1, weight = 1;

        for (int i = 0; i < zoomLevel; i++)
        {
            height *= 2;
            weight *= 2;
        }

        return new MapSize(height, weight);
    }
}