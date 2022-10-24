using System.Drawing;
using System.Drawing.Imaging;

namespace RequestsHub.Application.ImageServices;

internal class MergerSquareImages
{
    private const int sizeBeforeImprovement = 256;
    private const int improvement = 50;

    private readonly int factor;
    private readonly int resultSize;

    public MergerSquareImages(double dpiImprovementPercent)
    {
        if (dpiImprovementPercent > 1 || dpiImprovementPercent < 0)
        {
            throw new ArgumentException("Value must be between 0 and 1");
        }

        factor = Convert.ToInt32(dpiImprovementPercent * improvement);
        resultSize = sizeBeforeImprovement * factor;
    }

    public Bitmap Merge(ImageSet source)
    {
        Stopwatch stopWatch = new Stopwatch();
        Console.WriteLine("Start merge!");

        Bitmap bitmap = new(resultSize, resultSize, PixelFormat.Format24bppRgb);

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

                    var height = image.Height * factor / countVerticals;
                    var width = image.Width * factor / countHorizontals;

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

    public Bitmap Merge(string resourcePath)
    {
        Stopwatch stopWatch = new Stopwatch();
        Console.WriteLine("Start merge!");

        Bitmap bitmap = new(resultSize, resultSize, PixelFormat.Format24bppRgb);

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

                    height = image.Height * factor / verticals.Count;
                    width = image.Width * factor / horizontal.Count;

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