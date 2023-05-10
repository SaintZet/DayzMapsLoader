using DayzMapsLoader.Core.Contracts.Services;
using DayzMapsLoader.Core.Helpers;
using DayzMapsLoader.Core.Helpers.WebpDecoder;
using DayzMapsLoader.Shared.Enums;
using DayzMapsLoader.Shared.Wrappers;

using System.Drawing;
using System.Drawing.Imaging;

namespace DayzMapsLoader.Core.Services;

internal class MapMergeService : IMapMergeService
{
    private readonly int _sizeImprovementPercent;
    private readonly MapSize _mapSize;

    public MapMergeService(MapSize mapSize, int sizeImprovementPercent)
    {
        _mapSize = mapSize;

        if (sizeImprovementPercent > 100 || sizeImprovementPercent < 0)
            throw new ArgumentException("Value must be between 0 and 100");

        _sizeImprovementPercent = sizeImprovementPercent;
    }

    public static Bitmap WebpToBitmap(byte[] bytes)
    {
        using WebP webp = new();

        return webp.Decode(bytes);
    }

    public Bitmap Merge(MapParts source, ImageExtension extension)
    {
        int resultWidth = _mapSize.Width;
        int resultHeight = _mapSize.Height;

        if (_sizeImprovementPercent != 0)
        {
            resultWidth *= _sizeImprovementPercent;
            resultHeight *= _sizeImprovementPercent;
        }

        Bitmap result = new(resultWidth, resultHeight, PixelFormat.Format32bppArgb);

        using (Graphics graphic = Graphics.FromImage(result))
        {
            int countVerticals = source.Weight;
            int countHorizontals = source.Height;

            for (int y = 0; y < countVerticals; y++)
            {
                for (int x = 0; x < countHorizontals; x++)
                {
                    using Bitmap image = GetCorrectBitmap(source.GetPartOfMap(x, y), extension);

                    int widthPartResultImage, heightPartResultImage;

                    if (_sizeImprovementPercent != 0)
                    {
                        widthPartResultImage = image.Width * _sizeImprovementPercent / countVerticals;
                        heightPartResultImage = image.Height * _sizeImprovementPercent / countHorizontals;
                    }
                    else
                    {
                        widthPartResultImage = image.Width;
                        heightPartResultImage = image.Height;
                    }

                    using Bitmap resizedImage = ImageResizer.Resize(image, widthPartResultImage, heightPartResultImage);

                    widthPartResultImage = x * widthPartResultImage;
                    heightPartResultImage = y * heightPartResultImage;

                    graphic.DrawImage(resizedImage, widthPartResultImage, heightPartResultImage);
                }
            }
            graphic.Save();
        }

        return result;
    }

    private static Bitmap GetCorrectBitmap(MapPart mapPart, ImageExtension extension)
    {
        switch (extension)
        {
            case ImageExtension.png:
            case ImageExtension.jpg:
                return new Bitmap(mapPart.AsStream());

            case ImageExtension.webp:
                return WebpToBitmap(mapPart.Data);

            default:
                throw new NotImplementedException();
        }
    }
}