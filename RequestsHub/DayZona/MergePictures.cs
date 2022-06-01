using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayZona
{
    internal class MergePictures
    {
        public Image[][] GetPicturesFromFolder()
        {
            //List<Image> images = new List<Image>();

            Image[][] images = null;

            DirectoryInfo folderDir = new DirectoryInfo(@"D:\LivoniaMap\");

            List<DirectoryInfo> targetDirectories = folderDir.GetDirectories().ToList();
            List<string> targetDirectoriesName = targetDirectories.Select(d => d.FullName).OrderBy(s=>s.Length).ThenBy(s=>s).ToList<string>();
         
            using (Bitmap bit = new Bitmap(30000, 23622, PixelFormat.Format24bppRgb))
            {
                using (Graphics g = Graphics.FromImage(bit))
                {
                    int x, y = 0;
                    foreach(var directoryString in targetDirectoriesName)
                    {
                        x = 0;
                        DirectoryInfo directory = new DirectoryInfo(directoryString); 
                        foreach (var pictureFullName in directory.GetFiles().Select(s => s.FullName).OrderBy(s => s.Length).ThenBy(s => s))
                        {
                            Image resizedImage = ResizeImage(Image.FromFile(pictureFullName), 234, 186);
                            g.DrawImage(resizedImage
                                        , x == 0 ? 0 : x * 234
                                        , y == 0 ? 0 : y * 186);
                            Console.WriteLine(pictureFullName + " was placed on " + x * 234 + ", " + y * 186);
                            x++;
                        }
                        y++;
                    }
                    g.Save();
                }
                bit.Save(@"D:/testmerge.jpeg", ImageFormat.Jpeg);
            }


                return images;
        }
        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            Rectangle destRect = new Rectangle(0, 0, width, height);
            Bitmap destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
        
    }
}