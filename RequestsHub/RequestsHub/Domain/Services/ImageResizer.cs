using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace RequestsHub.Domain.Services
{
    internal class ImageResizer
    {
        public Bitmap Resize(Image image, int width, int height)
        {
            using Bitmap resizedPicture = new Bitmap(width, height);
            resizedPicture.SetResolution(image.HorizontalResolution, image.VerticalResolution);
            using (Graphics graphics = Graphics.FromImage(resizedPicture))
            {
                GraphicsSettings(graphics);

                using ImageAttributes wrapMode = new ImageAttributes();
                wrapMode.SetWrapMode(WrapMode.TileFlipXY);

                Rectangle rectangle = new(0, 0, width, height);

                graphics.DrawImage(image, rectangle, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
            }

            return resizedPicture;
        }

        private Graphics GraphicsSettings(Graphics graphics)
        {
            graphics.CompositingMode = CompositingMode.SourceCopy;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            return graphics;
        }
    }
}