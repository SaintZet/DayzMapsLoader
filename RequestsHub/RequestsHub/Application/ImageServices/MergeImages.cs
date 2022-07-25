using System.Drawing;
using System.Drawing.Imaging;

namespace RequestsHub.Application.Services.ImageServices;

internal class MergeSquareImages
{
    private const int sizeBeforeImprovement = 256;
    private const int improvement = 50;

    private readonly int factor;
    private readonly int resultSize;

    public MergeSquareImages(double dpiImprovementPercent)
    {
        if (dpiImprovementPercent > 1 || dpiImprovementPercent < 0)
        {
            throw new ArgumentException("Value must be between 0 and 1");
        }

        factor = Convert.ToInt32(dpiImprovementPercent * improvement);
        resultSize = sizeBeforeImprovement * factor;
    }

    public Bitmap Merge(byte[,][] source)
    {
        Bitmap bitmap = new(resultSize, resultSize, PixelFormat.Format24bppRgb);

        using (Graphics graphic = Graphics.FromImage(bitmap))
        {
            Image image;
            int height, width;
            int countVerticals = source.GetLength(0);
            int countHorizontals = source.GetLength(1);

            //MemoryStream ms = new();

            for (int y = 0; y < countVerticals; y++)
            {
                for (int x = 0; x < countHorizontals; x++)
                {
                    //TODO: try to dont make every time new ms.
                    image = Image.FromStream(new MemoryStream(source[x, y]));

                    height = image.Height * factor / countVerticals;
                    width = image.Width * factor / countHorizontals;

                    image = ImageResizer.Resize(image, width, height);

                    height = y == 0 ? 0 : y * height;
                    width = x == 0 ? 0 : x * width;

                    graphic.DrawImage(image, width, height);
                }
            }
            graphic.Save();
        }

        return bitmap;
    }

    public Bitmap Merge(string resourcePath)
    {
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