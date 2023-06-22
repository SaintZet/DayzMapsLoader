using DayzMapsLoader.Core.Contracts.Builders;
using DayzMapsLoader.Core.Helpers;
using DayzMapsLoader.Core.Helpers.WebpDecoder;
using DayzMapsLoader.Core.Enums;
using DayzMapsLoader.Core.Models;

using System.Drawing;
using System.Drawing.Imaging;

namespace DayzMapsLoader.Core.Builders;

internal class MapBuilder : IMapBuilder
{
    private readonly int _sizeImprovementPercent;
    private readonly MapSize _mapSize;

    public MapBuilder(MapSize mapSize, int sizeImprovementPercent)
    {
        if (sizeImprovementPercent is > 100 or < 0)
            throw new ArgumentException("Value must be between 0 and 100");
        
        _mapSize = mapSize;
        _sizeImprovementPercent = sizeImprovementPercent;
    }

    public Bitmap Build(MapParts source, ImageExtension extension)
    {
        var resultWidth = _mapSize.Width;
        var resultHeight = _mapSize.Height;

        if (_sizeImprovementPercent != 0)
        {
            resultWidth *= _sizeImprovementPercent;
            resultHeight *= _sizeImprovementPercent;
        }

        Bitmap result = new(resultWidth, resultHeight, PixelFormat.Format32bppArgb);

        using var graphic = Graphics.FromImage(result);
        var countVerticals = source.Weight;
        var countHorizontals = source.Height;

        for (var y = 0; y < countVerticals; y++)
        {
            for (var x = 0; x < countHorizontals; x++)
            {
                using var image = GetCorrectBitmap(source.GetPartOfMap(x, y), extension);

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

                using var resizedImage = ImageResizer.Resize(image, widthPartResultImage, heightPartResultImage);

                widthPartResultImage = x * widthPartResultImage;
                heightPartResultImage = y * heightPartResultImage;

                graphic.DrawImage(resizedImage, widthPartResultImage, heightPartResultImage);
            }
        }
        graphic.Save();

        return result;
    }

    private static Bitmap GetCorrectBitmap(MapPart mapPart, ImageExtension extension) =>
        extension switch
        {
            ImageExtension.png or ImageExtension.jpg => new Bitmap(mapPart.AsStream()),
            ImageExtension.webp => WebpToBitmap(mapPart.Data),
            _ => throw new NotImplementedException(),
        };
    
    public static Bitmap WebpToBitmap(byte[] bytes)
    {
        using WebP webp = new();

        return webp.Decode(bytes);
    }
}