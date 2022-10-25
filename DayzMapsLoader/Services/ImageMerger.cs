using DayzMapsLoader.DataTypes;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.Versioning;

namespace DayzMapsLoader.Services;

[SupportedOSPlatform("windows")]
internal class MergerSquareImages
{
    private const int _sizeBeforeImprovement = 256;
    private const int _improvement = 50;

    private readonly int _factor;
    private readonly int _resultSize;

    public MergerSquareImages(double dpiImprovementPercent)
    {
        if (dpiImprovementPercent > 1 || dpiImprovementPercent < 0)
        {
            throw new ArgumentException("Value must be between 0 and 1");
        }

        _factor = Convert.ToInt32(dpiImprovementPercent * _improvement);
        _resultSize = _sizeBeforeImprovement * _factor;
    }

    public Bitmap Merge(ImageSet source)
    {
        Stopwatch stopWatch = new Stopwatch();
        Console.WriteLine("Start merge!");

        Bitmap bitmap = new(_resultSize, _resultSize, PixelFormat.Format24bppRgb);

        using (Graphics graphic = Graphics.FromImage(bitmap))
        {
            int countVerticals = source.Weight;
            int countHorizontals = source.Height;

            for (int y = 0; y < countVerticals; y++)
            {
                for (int x = 0; x < countHorizontals; x++)
                {
                    //TODO: try to dont make every time new ms.
                    using var image = Image.FromStream(source.GetImage(x, y).AsStream());

                    var height = image.Height * _factor / countVerticals;
                    var width = image.Width * _factor / countHorizontals;

                    using var resizedImage = ImageResizer.Resize(image, width, height);

                    height = y == 0 ? 0 : y * height;
                    width = x == 0 ? 0 : x * width;

                    graphic.DrawImage(resizedImage, width, height);
                }
            }
            graphic.Save();
        }
        stopWatch.Stop();
        Console.WriteLine($"Merge time: {stopWatch.Elapsed}");

        return bitmap;
    }

    //TODO: Delete code duplicate.
    public Bitmap Merge(string resourcePath)
    {
        Stopwatch stopWatch = new Stopwatch();
        Console.WriteLine("Start merge!");

        Bitmap bitmap = new(_resultSize, _resultSize, PixelFormat.Format24bppRgb);

        using (Graphics graphic = Graphics.FromImage(bitmap))
        {
            Image image;
            int height, width;
            List<string> horizontal;
            List<string> verticals = GetMapVericals(resourcePath);

            for (int y = 0; y < verticals.Count; y++)
            {
                horizontal = GetMapHorizontal(verticals[y]);
                for (int x = 0; x < horizontal.Count; x++)
                {
                    image = Image.FromFile(horizontal[x]);

                    height = image.Height * _factor / verticals.Count;
                    width = image.Width * _factor / horizontal.Count;

                    image = ImageResizer.Resize(image, width, height);

                    width = x == 0 ? 0 : x * width;
                    height = y == 0 ? 0 : y * height;

                    graphic.DrawImage(image, width, height);
                }
            }
            graphic.Save();
        }
        stopWatch.Stop();
        Console.WriteLine($"Merge time: {stopWatch.Elapsed}");
        return bitmap;
    }

    private List<string> GetMapVericals(string resourcePath) => new DirectoryInfo(resourcePath).GetDirectories()
        .Select(d => d.FullName)
        .OrderBy(s => s.Length)
        .ThenBy(s => s)
        .ToList();

    private List<string> GetMapHorizontal(string currentDirectoryName) => new DirectoryInfo(currentDirectoryName).GetFiles()
        .Select(s => s.FullName)
        .OrderBy(s => s.Length)
        .ThenBy(s => s)
        .ToList();
}