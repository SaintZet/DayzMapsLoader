using DayzMapsLoader.Map;

namespace DayzMapsLoader.MapProviders.Helpers;

internal static class CalculateHelper
{
    public static Dictionary<int, MapSize> ZoomLevelRatioToSize(int zoomLevelQuantity)
    {
        int height = 1, weight = 1;

        Dictionary<int, MapSize> zoomLevels = new()
            {
                { 0, new MapSize(height, weight) }
            };

        for (int i = 1; i < zoomLevelQuantity; i++)
        {
            height *= 2;
            weight *= 2;
            zoomLevels.Add(i, new MapSize(height, weight));
        }

        return zoomLevels;
    }
}