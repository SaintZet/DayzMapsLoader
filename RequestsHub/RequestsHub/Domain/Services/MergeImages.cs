using RequestsHub.Domain.Services.ConsoleServices;
using System.Drawing;
using System.Drawing.Imaging;

namespace RequestsHub.Domain.Services
{
    internal class MergeImages
    {
        private readonly int xLength;
        private readonly int yLength;

        public MergeImages(int xLength = 25000, int yLength = 25000)
        {
            this.xLength = xLength;
            this.yLength = yLength;
        }

        public void MergeAndSave(Image[][] source, string PathSave)
        {
            int height, width;
            Image resizedImage;
            using Bitmap bitmap = new(xLength, yLength, PixelFormat.Format24bppRgb);
            using (Graphics graphic = Graphics.FromImage(bitmap))
            {
                for (int y = 0; y < source[y].Length; y++)
                {
                    for (int x = 0; x < source.Length; x++)
                    {
                        width = xLength / source[y].Length;
                        height = yLength / source.Length;
                        resizedImage = new ImageResizer().Resize(source[y][x], width, height);
                        width = x == 0 ? 0 : x * (xLength / source[y].Length);
                        height = y == 0 ? 0 : y * (yLength / source.Length);
                        graphic.DrawImage(resizedImage, width, height);
                    }
                }
                graphic.Save();
            }
            bitmap.Save(PathSave, ImageFormat.Bmp);
        }

        public void MergeAndSave(string resourcePath, string PathSave)
        {
            DirectoryInfo folderDir = new DirectoryInfo(resourcePath);

            List<string> verticals = folderDir.GetDirectories()
                                                            .Select(d => d.FullName)
                                                            .OrderBy(s => s.Length)
                                                            .ThenBy(s => s)
                                                            .ToList();
            int verticalCount = verticals.Count;
            Image image;
            List<string> horizontal;

            using Bitmap bitmap = new Bitmap(xLength, yLength, PixelFormat.Format24bppRgb);

            using (Graphics graphic = Graphics.FromImage(bitmap))
            {
                int height, width;

                for (int y = 0; y < verticals.Count; y++)
                {
                    //resource[y] it's currentDirectoryName
                    horizontal = GetMapPieces(verticals[y]);
                    for (int x = 0; x < horizontal.Count; x++)
                    {
                        height = yLength / verticals.Count;
                        width = xLength / horizontal.Count;
                        //horizontal[x] it's pictureFullName
                        image = Image.FromFile(horizontal[x]);
                        image = new ImageResizer().Resize(image, width, height);

                        width = x == 0 ? 0 : x * (xLength / horizontal.Count);
                        height = y == 0 ? 0 : y * (yLength / verticals.Count);

                        graphic.DrawImage(image, width, height);
                    }
                }
                graphic.Save();
            }
            bitmap.Save(PathSave, ImageFormat.Bmp);
        }

        private List<string> GetMapPieces(string currentDirectoryName) =>
            new DirectoryInfo(currentDirectoryName).GetFiles().Select(s => s.FullName).OrderBy(s => s.Length).ThenBy(s => s).ToList();
    }
}