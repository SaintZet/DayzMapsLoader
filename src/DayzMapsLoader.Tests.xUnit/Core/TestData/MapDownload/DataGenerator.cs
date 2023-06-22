namespace DayzMapsLoader.Tests.xUnit.Core.TestData.MapDownload;

internal static class DataGenerator
{
    private const int _maxProviderId = 2;
    private const int _maxMapId = 2;
    private const int _maxTypeId = 2;
    private const int _maxZoomLevel = 2;

    public static IEnumerable<object[]> YieldReturnAllStages()
    {
        for (var providerId = 1; providerId < _maxProviderId; providerId++)
            for (var mapId = 1; mapId < _maxMapId; mapId++)
                for (var typeId = 1; typeId < _maxTypeId; typeId++)
                    for (var zoomLevel = 1; zoomLevel < _maxZoomLevel; zoomLevel++)
                        yield return new object[] { providerId, mapId, typeId, zoomLevel };
    }

    public static IEnumerable<object[]> YieldReturnProviderAndZoom()
    {
        for (var providerId = 1; providerId < _maxProviderId; providerId++)
            for (var zoomLevel = 1; zoomLevel < _maxZoomLevel; zoomLevel++)
                yield return new object[] { providerId, zoomLevel };
    }
}