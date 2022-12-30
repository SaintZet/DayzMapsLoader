using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.Versioning;

namespace DayzMapsLoader.Application.Managers.MergerSquareImages.Helpers;

[SupportedOSPlatform("windows")]
internal static class ImageResizer
{
    public static Bitmap Resize(Bitmap image, int width, int height)
    {
        Rectangle destRect = new(0, 0, width, height);
        Bitmap resizedPicture = new(width, height);

        resizedPicture.SetResolution(image.HorizontalResolution, image.VerticalResolution);

        using (Graphics graphics = Graphics.FromImage(resizedPicture))
        {
            graphics.SetSettings();

            using ImageAttributes wrapMode = new();
            wrapMode.SetWrapMode(WrapMode.TileFlipXY);

            graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
        }

        return resizedPicture;
    }

    private static void SetSettings(this Graphics graphics)
    {
        graphics.CompositingMode = CompositingMode.SourceCopy;
        graphics.CompositingQuality = CompositingQuality.HighQuality;
        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        graphics.SmoothingMode = SmoothingMode.HighQuality;
        graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
    }
}