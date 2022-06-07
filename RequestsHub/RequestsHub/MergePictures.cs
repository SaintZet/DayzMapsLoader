using System.Drawing;
using System.Drawing.Imaging;

namespace RequestsHub
{
    internal class MergePictures
    {
        public void GetFullMap(int xLength = 12000, int yLength = 12000, string directoryPath = null, string finalPath = null, string fileName = "LivoniaMap.jpeg")
        {
            if (directoryPath is null || finalPath is null)
            {
                throw new Exception($"Parameter {nameof(directoryPath)} or {nameof(finalPath)} is null");
            }

            PictureResizer pictureResizer;
            MergePictureWorkingAnimation workingAnimation;

            DirectoryInfo folderDir = new DirectoryInfo(directoryPath);

            List<string> targetDirectoriesName = folderDir.GetDirectories()
                                                            .Select(d => d.FullName)
                                                            .OrderBy(s => s.Length)
                                                            .ThenBy(s => s)
                                                            .ToList<string>();
            DirectoryInfo fileDir = new DirectoryInfo(targetDirectoriesName[0]);
            int fileCountThatShouldBe = fileDir.GetFiles().Count();

            using (Bitmap bitmap = new Bitmap(xLength, yLength, PixelFormat.Format24bppRgb))
            {
                using (Graphics graphic = Graphics.FromImage(bitmap))
                {
                    int x, y = 0;
                    foreach (string currentDirectoryName in targetDirectoriesName)
                    {
                        x = 0;
                        workingAnimation = new MergePictureWorkingAnimation();
                        foreach (string pictureFullName in GetMapPieces(currentDirectoryName))
                        {
                            workingAnimation.Spin(currentDirectoryName);

                            pictureResizer = new PictureResizer();
                            Image resizedImage = pictureResizer.Resize(Image.FromFile(pictureFullName), (xLength / 128), (yLength / 128));

                            graphic.DrawImage(resizedImage
                                        , x == 0 ? 0 : x * (xLength / fileCountThatShouldBe)
                                        , y == 0 ? 0 : y * (yLength / targetDirectoriesName.Count()));
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

        //private bool IsMapSquare(List<string> directories, int fileCountThatShouldBe)
        //{
        //    foreach (string directoryName in directories)
        //    {
        //        DirectoryInfo directory = new DirectoryInfo(directoryName);

        // if (directory.GetFiles().Count() != fileCountThatShouldBe) { Console.WriteLine($"In
        // directory {directory.Name} files count equals {directory.GetFiles().Count()}"); return
        // false; } }

        //    return true;
        //}
    }
}