using DayzMapsLoader.Core.Contracts.Builders;
using DayzMapsLoader.Core.Helpers;
using DayzMapsLoader.Core.Helpers.WebpDecoder;
using DayzMapsLoader.Core.Enums;
using DayzMapsLoader.Core.Models;

using System.Drawing;
using System.Drawing.Imaging;

namespace DayzMapsLoader.Core.Builders;

internal class MapBuilder : IMapBuilder, IAsyncDisposable
{
    private readonly int _sizeImprovementPercent;
    private readonly Bitmap _result;
    private readonly Graphics _graphic;
    private readonly MapSize _ratioMapSize;

    public MapBuilder(MapSize mapSize, int zoom,  int sizeImprovementPercent)
    {
        if (sizeImprovementPercent is > 100 or < 0)
            throw new ArgumentException("Value must be between 0 and 100");
        
        _sizeImprovementPercent = sizeImprovementPercent;
        var resultWidth = mapSize.Width;
        var resultHeight = mapSize.Height;

        if (_sizeImprovementPercent != 0)
        {
	        resultWidth *= _sizeImprovementPercent;
	        resultHeight *= _sizeImprovementPercent;
        }

        _result = new Bitmap(resultWidth, resultHeight, PixelFormat.Format32bppArgb);

        _graphic = Graphics.FromImage(_result);
        _ratioMapSize = MapSize.ConvertZoomLevelRatioSize(zoom);
    }

    public void Append(MapPart part, ImageExtension extension)
    {
	    using var image = GetCorrectBitmap(part, extension);

	    int widthPartResultImage, heightPartResultImage;

	    if (_sizeImprovementPercent != 0)
	    {
		    widthPartResultImage = image.Width * _sizeImprovementPercent / _ratioMapSize.Height;
		    heightPartResultImage = image.Height * _sizeImprovementPercent / _ratioMapSize.Width;
	    }
	    else
	    {
		    widthPartResultImage = image.Width;
		    heightPartResultImage = image.Height;
	    }

	    using var resizedImage = ImageResizer.Resize(image, widthPartResultImage, heightPartResultImage);

	    widthPartResultImage = part.X * widthPartResultImage;
	    heightPartResultImage = part.Y * heightPartResultImage;

	    _graphic.DrawImage(resizedImage, widthPartResultImage, heightPartResultImage);
    }

    public Bitmap Build()
    {
        _graphic.Save();
        return _result;
    }

    private static Bitmap GetCorrectBitmap(MapPart mapPart, ImageExtension extension) =>
        extension switch
        {
            ImageExtension.png or ImageExtension.jpg => new Bitmap(mapPart.AsStream()),
            ImageExtension.webp => WebP.RawWebpToBitmap(mapPart.Data),
            _ => throw new NotImplementedException(),
        };

    public ValueTask DisposeAsync()
    {
	    _graphic.Dispose();
	    return new ValueTask();
    }
}