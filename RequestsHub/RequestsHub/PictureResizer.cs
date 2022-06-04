using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace RequestsHub
{
    interface IPictureResizer
    {
        Bitmap Resize(Image image, int width, int height);
        Graphics GraphicsSettings(Graphics graphics);
        
    }

    internal class PictureResizer : IPictureResizer
    {
        private Bitmap resizedPicture;


        Bitmap IPictureResizer.Resize(Image image, int width, int height)
        {
            Rectangle destRect = new Rectangle(0, 0, width, height);
            resizedPicture = new Bitmap(width, height);

            resizedPicture.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (Graphics graphics = Graphics.FromImage(resizedPicture))
            {
                GraphicsSettings(graphics);

                using (ImageAttributes wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return resizedPicture;
        }
        public Graphics GraphicsSettings(Graphics graphics)
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

