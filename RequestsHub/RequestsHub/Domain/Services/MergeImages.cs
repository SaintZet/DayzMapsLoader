using RequestsHub.Domain.Services.ConsoleServices;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;

namespace RequestsHub.Domain.Services
{
    internal class MergeImages
    {
        private readonly int xLength;
        private readonly int yLength;

        public MergeImages(int xLength = 1000, int yLength = 1000)
        {
            this.xLength = xLength;
            this.yLength = yLength;
        }

        public void MergeAndSave(byte[,][] map, string PathSave)
        {
            Stopwatch stopWatch = new();
            stopWatch.Start();

            int height, width;
            int countVerticals = map.GetLength(0);
            int countHorizontals = map.GetLength(1);
            Image resizedImage;

            using Bitmap bitmap = new(xLength, yLength, PixelFormat.Format24bppRgb);
            using (Graphics graphic = Graphics.FromImage(bitmap))
            {
                Console.Write("Merge ");
                using (ProgressBar progress = new())
                {
                    for (int y = 0; y < countVerticals; y++)
                    {
                        for (int x = 0; x < countHorizontals; x++)
                        {
                            height = yLength / countVerticals;
                            width = xLength / countHorizontals;

                            resizedImage = new ImageResizer().Resize(map[y, x], width, height);

                            height = y == 0 ? 0 : y * (yLength / countVerticals);
                            width = x == 0 ? 0 : x * (xLength / countHorizontals);

                            graphic.DrawImage(resizedImage, width, height);
                            progress.Report((double)(y * countHorizontals + x) / (countHorizontals * countVerticals));
                        }
                    }
                }
                graphic.Save();
            }
            bitmap.Save(PathSave, ImageFormat.Bmp);

            stopWatch.Stop();
            Console.WriteLine("time: {0}", stopWatch.Elapsed);
        }

        public void MergeAndSave(string resourcePath, string PathSave)
        {
            Stopwatch stopWatch = new();
            stopWatch.Start();

            Image image;
            List<string> horizontal;
            List<string> verticals = new DirectoryInfo(resourcePath).GetDirectories()
                                                            .Select(d => d.FullName)
                                                            .OrderBy(s => s.Length)
                                                            .ThenBy(s => s)
                                                            .ToList();
            int verticalCount = verticals.Count;
            int horizontalCount = horizontal.Count;

            using Bitmap bitmap = new(xLength, yLength, PixelFormat.Format24bppRgb);

            using (Graphics graphic = Graphics.FromImage(bitmap))
            {
                int height, width;

                Console.Write("Merge ");
                using (ProgressBar progress = new())
                {
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
                }
                graphic.Save();
            }
            bitmap.Save(PathSave, ImageFormat.Bmp);

            stopWatch.Stop();
            Console.WriteLine("time: {0}", stopWatch.Elapsed);
        }

        private List<string> GetMapPieces(string currentDirectoryName) =>
            new DirectoryInfo(currentDirectoryName).GetFiles().Select(s => s.FullName).OrderBy(s => s.Length).ThenBy(s => s).ToList();
    }
}