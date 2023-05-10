using DayzMapsLoader.Core.Services;
using DayzMapsLoader.Shared.Enums;
using DayzMapsLoader.Shared.Wrappers;

using System.Drawing;
using System.Reflection;
using System.Runtime.Versioning;

namespace DayzMapsLoader.Core.UnitTests.InternalManagers;

[SupportedOSPlatform("windows")]
public class MapMergeServiceTests
{
    private const string _generalPathToTestData = "DayzMapsLoader.Core.Tests.xUnit.TestData.MapMerge";

    private const string _fullImageTemaplatePath = $"{_generalPathToTestData}.{{0}}.Original.{{0}}";
    private const string _partTemaplatePath = $"{_generalPathToTestData}.{{0}}.{{1}},{{2}}.{{0}}";

    private const int _imageCountVertical = 2;
    private const int _imageCountHorizontal = 2;

    private const int _sizeImprovementPercent = 0;

    private enum BitmapCompareResult
    {
        CompareOk,
        PixelMismatch,
        SizeMismatch
    };

    public static IEnumerable<object[]> GetTestData()
    {
        foreach (ImageExtension extension in (ImageExtension[])Enum.GetValues(typeof(ImageExtension)))
        {
            yield return new object[] { extension };
        }
    }

    [Theory]
    [Trait("Category", "Unit")]
    [MemberData(nameof(GetTestData))]
    public void Test_MergeImage_ShouldProduceExpectedResult(ImageExtension extension)
    {
        // Arrange
        Bitmap originalImage = GetOriginalImage(extension);

        var mapSize = new MapSize(originalImage.Height, originalImage.Width);
        var imageMerger = new MapMergeService(mapSize, _sizeImprovementPercent);

        var imageParts = new MapParts(new MapSize(_imageCountHorizontal, _imageCountVertical));

        for (int y = 0; y < _imageCountVertical; y++)
        {
            for (int x = 0; x < _imageCountHorizontal; x++)
            {
                string fileName = string.Format(_partTemaplatePath, extension.ToString(), y, x);

                var bytes = GetByteArrayFromEmbeddedResource(fileName);

                var imagePart = new MapPart(bytes);

                imageParts.AddPart(x, y, imagePart);
            }
        }

        //Act
        var resultBitmap = imageMerger.Merge(imageParts, extension);

        //Assert
        var resultCompare = Compare(originalImage, resultBitmap);

        Assert.Equal(BitmapCompareResult.CompareOk, resultCompare);
    }

    private static Bitmap GetOriginalImage(ImageExtension extension)
    {
        string originalImagePath = string.Format(_fullImageTemaplatePath, extension.ToString());

        if (extension == ImageExtension.webp)
        {
            var webP = GetByteArrayFromEmbeddedResource(originalImagePath);

            return MapMergeService.WebpToBitmap(webP);
        }

        var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(originalImagePath)!;

        return new Bitmap(stream);
    }

    private static byte[] GetByteArrayFromEmbeddedResource(string fileName)
    {
        using Stream myStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(fileName)!;

        byte[] byteArray = new byte[myStream!.Length];

        myStream.Read(byteArray, 0, byteArray.Length);

        return byteArray;
    }

    private static BitmapCompareResult Compare(Bitmap bmp1, Bitmap bmp2)
    {
        if (bmp1.Size != bmp2.Size)
        {
            return BitmapCompareResult.SizeMismatch;
        }

        //Sizes are the same so start comparing pixels
        for (int x = 0; x < bmp1.Width; x++)
        {
            for (int y = 0; y < bmp1.Height; y++)
            {
                var bmp1Pixel = bmp1.GetPixel(x, y);
                var bmp2Pixel = bmp2.GetPixel(x, y);

                //Get difference between colors
                var diff = CompareColors(bmp1Pixel, bmp2Pixel);
                if (diff > 1)
                    return BitmapCompareResult.PixelMismatch;
            }
        }

        return BitmapCompareResult.CompareOk;
    }

    private static double CompareColors(Color a, Color b)
    {
        return Math.Abs((Math.Abs(a.R - b.R) + Math.Abs(a.G - b.G) + Math.Abs(a.B - b.B)) / (255.0 * 3));
    }
}