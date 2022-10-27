using DayzMapsLoader.Helpers.WebpDecoder;
using DayzMapsLoader.Map;
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

    public Bitmap Merge(MapInfo mapInfo, MapParts source)
    {
        Bitmap result = new(_resultSize, _resultSize, PixelFormat.Format24bppRgb);

        using (Graphics graphic = Graphics.FromImage(result))
        {
            int countVerticals = source.Weight;
            int countHorizontals = source.Height;

            for (int y = 0; y < countVerticals; y++)
            {
                for (int x = 0; x < countHorizontals; x++)
                {
                    using Bitmap image = GetCorrectBitmap(mapInfo, source.GetPartOfMap(x, y));

                    var height = image.Height * _factor / countVerticals;
                    var width = image.Width * _factor / countHorizontals;

                    using Bitmap resizedImage = ImageResizer.Resize(image, width, height);

                    height = y * height;
                    width = x * width;

                    graphic.DrawImage(resizedImage, width, height);
                }
            }
            graphic.Save();
        }

        return result;
    }

    //TODO: Delete code duplicate.
    public Bitmap Merge(string resourcePath)
    {
        Bitmap bitmap = new(_resultSize, _resultSize, PixelFormat.Format24bppRgb);

        using (Graphics graphic = Graphics.FromImage(bitmap))
        {
            List<string> horizontal;
            List<string> verticals = GetMapVericals(resourcePath);

            for (int y = 0; y < verticals.Count; y++)
            {
                horizontal = GetMapHorizontal(verticals[y]);
                for (int x = 0; x < horizontal.Count; x++)
                {
                    Bitmap image = new(horizontal[x]);

                    var height = image.Height * _factor / verticals.Count;
                    var width = image.Width * _factor / horizontal.Count;

                    using var resizedImage = ImageResizer.Resize(image, width, height);

                    width = x * width;
                    height = y * height;

                    graphic.DrawImage(resizedImage, width, height);
                }
            }
            graphic.Save();
        }

        return bitmap;
    }

    private Bitmap GetCorrectBitmap(MapInfo mapInfo, MapPart mapPart)
    {
        switch (mapInfo.MapExtension)
        {
            case MapExtension.png:
            case MapExtension.jpg:
                return new Bitmap(mapPart.AsStream());

            case MapExtension.webp:
                return WebPFormat
                //using (WebP webp = new())
                //    return webp.Decode(mapPart.Data);

            default:
                throw new NotImplementedException();
        }
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