using RequestsHub.Domain.Services.ConsoleServices;
using System.Drawing;
using System.Drawing.Imaging;

namespace RequestsHub.Domain.Services
{
    internal class MergeImages
    {
        public void GetFullMap(int xLength = 12000, int yLength = 12000, string directoryPath = null, string finalPath = null, string fileName = "LivoniaMap.jpeg")
        {
            if (directoryPath is null || finalPath is null)
            {
                throw new Exception($"Parameter {nameof(directoryPath)} or {nameof(finalPath)} is null");
            }

            DirectoryInfo folderDir = new DirectoryInfo(directoryPath);

            List<string> targetDirectoriesName = folderDir.GetDirectories()
                                                            .Select(d => d.FullName)
                                                            .OrderBy(s => s.Length)
                                                            .ThenBy(s => s)
                                                            .ToList();
            DirectoryInfo fileDir = new DirectoryInfo(targetDirectoriesName[0]);
            int fileCountThatShouldBe = fileDir.GetFiles().Length;

            using (Bitmap bitmap = new Bitmap(xLength, yLength, PixelFormat.Format24bppRgb))
            {
                using (Graphics graphic = Graphics.FromImage(bitmap))
                {
                    int x, y = 0;
                    foreach (string currentDirectoryName in targetDirectoriesName)
                    {
                        x = 0;
                        ConsoleAnimation workingAnimation = new();
                        foreach (string pictureFullName in GetMapPieces(currentDirectoryName))
                        {
                            workingAnimation.Spin(currentDirectoryName);

                            var image = Image.FromFile(pictureFullName);
                            var width = xLength / 128;
                            var height = yLength / 128;

                            Image resizedImage = new ImageResizer().Resize(image, width, height);

                            width = x == 0 ? 0 : x * (xLength / fileCountThatShouldBe);
                            height = y == 0 ? 0 : y * (yLength / targetDirectoriesName.Count());
                            graphic.DrawImage(resizedImage, width, height);

                            x++;
                        }
                        y++;
                    }
                    graphic.Save();
                }
                bitmap.Save(finalPath + fileName, ImageFormat.Jpeg);
            }
        }

        private IEnumerable<string> GetMapPieces(string currentDirectoryName) => new DirectoryInfo(currentDirectoryName).GetFiles()
                                                                                                                      .Select(s => s.FullName)
                                                                                                                      .OrderBy(s => s.Length)
                                                                                                                      .ThenBy(s => s);
    }
}