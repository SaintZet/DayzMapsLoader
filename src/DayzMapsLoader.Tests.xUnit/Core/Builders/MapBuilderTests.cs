using System.Drawing;
using System.Runtime.Versioning;
using DayzMapsLoader.Core.Builders;
using DayzMapsLoader.Core.Enums;
using DayzMapsLoader.Core.Models;
using DayzMapsLoader.Tests.xUnit.Core.TestData.Images;

namespace DayzMapsLoader.Tests.xUnit.Core.Builders;

[SupportedOSPlatform("windows")]
public class MapBuilderTests
{

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
    public async Task Test_MergeImage_ShouldProduceExpectedResult(ImageExtension extension)
    {
        // Arrange
        var originalImage = ImageDataProvider.GetOriginalImage(extension);

        var mapSize = new MapSize(originalImage.Height, originalImage.Width);
        await using var mapBuilder = new MapBuilder(mapSize, 1, _sizeImprovementPercent);

        for (var y = 0; y < _imageCountVertical; y++)
        {
            for (var x = 0; x < _imageCountHorizontal; x++)
            {
                var bytes = ImageDataProvider.GetByteArrayFromEmbeddedResource(extension.ToString(), x , y);
			
                var imagePart = new MapPart(x, y, bytes);
				mapBuilder.Append(imagePart, extension);
            }
        }

        //Act
        var resultBitmap = mapBuilder.Build();

        //Assert
        var resultCompare = Compare(originalImage, resultBitmap);

        Assert.Equal(BitmapCompareResult.CompareOk, resultCompare);
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