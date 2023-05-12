namespace DayzMapsLoader.Core.Tests.xUnit.TestData.MapDownload;

internal static class DataGenerator
{
    private static readonly int _maxProviderId = 2;
    private static readonly int _maxMapId = 2;
    private static readonly int _maxTypeId = 2;
    private static readonly int _maxZoomLevel = 2;

    public static IEnumerable<object[]> YieldReturnAllStages()
    {
        for (int providerId = 1; providerId < _maxProviderId; providerId++)
            for (int mapId = 1; mapId < _maxMapId; mapId++)
                for (int typeId = 1; typeId < _maxTypeId; typeId++)
                    for (int zoomLevel = 1; zoomLevel < _maxZoomLevel; zoomLevel++)
                        yield return new object[] { providerId, mapId, typeId, zoomLevel };
    }

    public static IEnumerable<object[]> YieldReturnProviderAndZoom()
    {
        for (int providerId = 1; providerId < _maxProviderId; providerId++)
            for (int zoomLevel = 1; zoomLevel < _maxZoomLevel; zoomLevel++)
                yield return new object[] { providerId, zoomLevel };
    }
}