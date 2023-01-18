//using DayzMapsLoader.Application.Abstractions.Services;
//using DayzMapsLoader.Application.Services;
//using DayzMapsLoader.Domain.Entities.Map;
//using DayzMapsLoader.Domain.Entities.MapProvider;
//using DayzMapsLoader.Infrastructure.DbContexts;
//using System.Runtime.Versioning;

//namespace DayzMapsLoader.Application.UnitTests.Services;

//[SupportedOSPlatform("windows")]
//public class MapDownloaderTest
//{
//    private const int MaxZoomLevelForTest = 2;
//    private const int ImageRate = 25;

//    [Theory]
//    [MemberData(nameof(GetTestData))]
//    public void DownloadMap(MapProviderName provider, MapType mapType, MapName mapName, int zoomLevel)
//    {
//        var loader = InitializeDefaultMapDownloader();
//        loader.MapProviderName = provider;

//        var bitmap = loader.DownloadMap(mapName, mapType, zoomLevel);

//        var metaData = new MapImageMetaData
//        {
//            Width = bitmap.Width,
//            Height = bitmap.Height,
//            HorizontalResolution = bitmap.HorizontalResolution,
//            VerticalResolution = bitmap.VerticalResolution,
//        };

//        var expectedMetaData = new MapImageMetaData
//        {
//            Width = 6400,
//            Height = 6400,
//            HorizontalResolution = 96,
//            VerticalResolution = 96,
//        };

//        Assert.Equal(expectedMetaData, metaData);
//    }

//    private static IEnumerable<object[]> GetTestData()
//    {
//        foreach (MapProviderName provider in (MapProviderName[])Enum.GetValues(typeof(MapProviderName)))
//        {
//            foreach (MapType mapType in (MapType[])Enum.GetValues(typeof(MapType)))
//            {
//                for (int zoomLevel = 0; zoomLevel < MaxZoomLevelForTest; zoomLevel++)
//                {
//                    foreach (MapName map in (MapName[])Enum.GetValues(typeof(MapName)))
//                    {
//                        yield return new object[] { provider, mapType, map, zoomLevel };
//                    }
//                }
//            }
//        }
//    }

//    private static IMapDownloader InitializeDefaultMapDownloader()
//    {
//        return new MapDownloader(new JsonMapsDbContext())
//        {
//            QualityMultiplier = ImageRate,
//        };
//    }

//    private struct MapImageMetaData
//    {
//        public int Width;
//        public int Height;
//        public float HorizontalResolution;
//        public float VerticalResolution;
//    }
//}