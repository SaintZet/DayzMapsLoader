﻿using DayzMapsLoader.Application.Helpers;
using DayzMapsLoader.Application.Helpers.WebpDecoder;
using DayzMapsLoader.Domain.Entities.Map;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.Versioning;

namespace DayzMapsLoader.Application.Managers;

[SupportedOSPlatform("windows")]
public class ImageMerger
{
    private int _dpiImprovementPercent;

    public ImageMerger(MapSize mapSize, int dpiImprovementPercent)
    {
        MapSize = mapSize;
        DpiImprovementPercent = dpiImprovementPercent;
    }

    public MapSize MapSize { get; set; }

    public int DpiImprovementPercent
    {
        get => _dpiImprovementPercent;
        set
        {
            if (value > 100 || value < 0)
            {
                throw new ArgumentException("Value must be between 0 and 100");
            }

            _dpiImprovementPercent = value;
        }
    }

    public static Bitmap WebpToBitmap(byte[] bytes)
    {
        using WebP webp = new();

        return webp.Decode(bytes);
    }

    public Bitmap Merge(MapParts source, ImageExtension extension)
    {
        var mapSize = MapSize;

        if (_dpiImprovementPercent != 0)
        {
            mapSize.Width *= _dpiImprovementPercent;
            mapSize.Height *= _dpiImprovementPercent;
        }

        Bitmap result = new(mapSize.Width, mapSize.Height, PixelFormat.Format32bppArgb);

        using (Graphics graphic = Graphics.FromImage(result))
        {
            int countVerticals = source.Weight;
            int countHorizontals = source.Height;

            for (int y = 0; y < countVerticals; y++)
            {
                for (int x = 0; x < countHorizontals; x++)
                {
                    using Bitmap image = GetCorrectBitmap(source.GetPartOfMap(x, y), extension);

                    int width, height;

                    if (_dpiImprovementPercent != 0)
                    {
                        width = image.Width * _dpiImprovementPercent / countVerticals;
                        height = image.Height * _dpiImprovementPercent / countHorizontals;
                    }
                    else
                    {
                        width = image.Width;
                        height = image.Height;
                    }

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