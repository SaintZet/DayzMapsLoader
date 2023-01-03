using DayzMapsLoader.Application.Managers;
using DayzMapsLoader.Domain.Entities.Map;
using System.Drawing;
using System.Reflection;
using System.Runtime.Versioning;

namespace DayzMapsLoader.Application.UnitTests.InternalManagers;

[SupportedOSPlatform("windows")]
public class ImageMergerTest
{
    private const string GeneralPathToTestData = "DayzMapsLoader.Application.UnitTests.TestData.ImageMerger";

    private const string FullImageTemaplatePath = $"{GeneralPathToTestData}.{{0}}.Original.{{0}}";
    private const string PartTemaplatePath = $"{GeneralPathToTestData}.{{0}}.{{1}},{{2}}.{{0}}";

    private const int ImageCountVertical = 2;
    private const int ImageCountHorizontal = 2;

    private enum BitmapCompareResult
    {
        CompareOk,
        PixelMismatch,
        SizeMismatch
    };

    [Theory]
    [MemberData(nameof(GetTestData))]
    public void MergeImage(ImageExtension extension)
    {
        Bitmap originalImage = GetOriginalImage(extension);

        var mapSize = new Domain.Entities.Map.Size(originalImage.Height, originalImage.Width);
        var imageMerger = new ImageMerger(mapSize, 0);

        var imageParts = new MapParts(new Domain.Entities.Map.Size(2));

        for (int y = 0; y < ImageCountVertical; y++)
        {
            for (int x = 0; x < ImageCountHorizontal; x++)
            {
                string fileName = string.Format(PartTemaplatePath, extension.ToString(), y, x);

                var bytes = GetByteArrayFromEmbeddedResource(fileName);

                var imagePart = new MapPart(bytes);

                imageParts.AddPart(x, y, imagePart);
            }
        }
        var resultBitmap = imageMerger.Merge(imageParts, extension);

        var resultCompare = Compare(originalImage, resultBitmap);

        Assert.Equal(BitmapCompareResult.CompareOk, resultCompare);
    }

    private static IEnumerable<object[]> GetTestData()
    {
        foreach (ImageExtension extension in (ImageExtension[])Enum.GetValues(typeof(ImageExtension)))
        {
            yield return new object[] { extension };
        }
    }

    private static Bitmap GetOriginalImage(ImageExtension extension)
    {
        string originalImagePath = string.Format(FullImageTemaplatePath, extension.ToString());

        if (extension == ImageExtension.webp)
        {
            var webP = GetByteArrayFromEmbeddedResource(originalImagePath);

            return ImageMerger.WebpToBitmap(webP);
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