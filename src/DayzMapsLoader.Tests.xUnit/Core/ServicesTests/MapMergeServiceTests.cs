using DayzMapsLoader.Core.Builders;
using DayzMapsLoader.Core.Enums;
using DayzMapsLoader.Core.Models;

using System.Drawing;
using System.Reflection;
using System.Runtime.Versioning;

namespace DayzMapsLoader.Tests.xUnit.Core.ServicesTests;

[SupportedOSPlatform("windows")]
public class MapMergeServiceTests
{
    private const string _generalPathToTestData = "DayzMapsLoader.Tests.xUnit.Core.TestData.MapMerge";

    private const string _fullImageTemplatePath = $"{_generalPathToTestData}.{{0}}.Original.{{0}}";
    private const string _partTemplatePath = $"{_generalPathToTestData}.{{0}}.{{1}},{{2}}.{{0}}";

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
        foreach (var extension in (ImageExtension[])Enum.GetValues(typeof(ImageExtension)))
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
        var originalImage = GetOriginalImage(extension);

        var mapSize = new MapSize(originalImage.Height, originalImage.Width);
        var imageMerger = new MapBuilder(mapSize, _sizeImprovementPercent);

        var imageParts = new MapParts(new MapSize(_imageCountHorizontal, _imageCountVertical));

        for (var y = 0; y < _imageCountVertical; y++)
        {
            for (var x = 0; x < _imageCountHorizontal; x++)
            {
                var fileName = string.Format(_partTemplatePath, extension.ToString(), y, x);

                var bytes = GetByteArrayFromEmbeddedResource(fileName);

                var imagePart = new MapPart(bytes);

                imageParts.AddPart(x, y, imagePart);
            }
        }

        //Act
        var resultBitmap = imageMerger.Build(imageParts, extension);

        //Assert
        var resultCompare = Compare(originalImage, resultBitmap);

        Assert.Equal(BitmapCompareResult.CompareOk, resultCompare);
    }

    private static Bitmap GetOriginalImage(ImageExtension extension)
    {
        var originalImagePath = string.Format(_fullImageTemplatePath, extension.ToString());

        if (extension == ImageExtension.webp)
        {
            var webP = GetByteArrayFromEmbeddedResource(originalImagePath);

            return MapBuilder.WebpToBitmap(webP);
        }

        var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(originalImagePath)!;

        return new Bitmap(stream);
    }

    private static byte[] GetByteArrayFromEmbeddedResource(string fileName)
    {
        using var myStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(fileName)!;

        var byteArray = new byte[myStream!.Length];

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
        for (var x = 0; x < bmp1.Width; x++)
        {
            for (var y = 0; y < bmp1.Height; y++)
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
        => Math.Abs((Math.Abs(a.R - b.R) + Math.Abs(a.G - b.G) + Math.Abs(a.B - b.B)) / (255.0 * 3));
}