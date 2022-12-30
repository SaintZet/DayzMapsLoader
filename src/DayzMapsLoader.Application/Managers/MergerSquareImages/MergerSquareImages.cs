using DayzMapsLoader.Application.Managers.MergerSquareImages.Helpers;
using DayzMapsLoader.Application.Managers.MergerSquareImages.Helpers.WebpDecoder;
using DayzMapsLoader.Domain.Entities.Map;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.Versioning;

namespace DayzMapsLoader.Application.Managers.MergerSquareImages;

[SupportedOSPlatform("windows")]
public class MergerSquareImages
{
    private const int _sizeBeforeImprovement = 256;
    private const int _improvement = 50;

    private int _factor;
    private int _resultSize;

    private double _dpiImprovementPercent;

    public MergerSquareImages(double dpiImprovementPercent)
    {
        DpiImprovementPercent = dpiImprovementPercent;
    }

    public double DpiImprovementPercent
    {
        get => _dpiImprovementPercent;
        set
        {
            if (value > 1 || value < 0)
            {
                throw new ArgumentException("Value must be between 0 and 1");
            }

            _factor = Convert.ToInt32(value * _improvement);
            _resultSize = _sizeBeforeImprovement * _factor;

            _dpiImprovementPercent = value;
        }
    }

    public Bitmap Merge(MapParts source, ImageExtension extension)
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
                    using Bitmap image = GetCorrectBitmap(extension, source.GetPartOfMap(x, y));

                    var width = image.Width * _factor / countVerticals;
                    var height = image.Height * _factor / countHorizontals;

                    using Bitmap resizedImage = ImageResizer.Resize(image, width, height);

                    width = x * width;
                    height = y * height;

                    graphic.DrawImage(resizedImage, width, height);
                }
            }
            graphic.Save();
        }

        return result;
    }

    private static Bitmap GetCorrectBitmap(ImageExtension extension, MapPart mapPart)
    {
        switch (extension)
        {
            case ImageExtension.png:
            case ImageExtension.jpg:
                return new Bitmap(mapPart.AsStream());

            case ImageExtension.webp:
                using (WebP webp = new())
                    return webp.Decode(mapPart.Data);

            default:
                throw new NotImplementedException();
        }
    }
}