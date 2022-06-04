using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestsHub
{
    internal class MergePictures
    {
        public void GetFullMap(int xLength = 12000, int yLength = 12000)
        {
            const string directoryPath = @"D:\LivoniaMap\";
            const string finalPath = @"C:/Users/repos/SurviveRunner/SaintZet/SurviveRunner/AdvancedZoomAndPanSample/LivoniaMap.jpg";
            IPictureResizer pictureResizer;
            IMergePictureWorkingAnimation workingAnimation;

            DirectoryInfo folderDir = new DirectoryInfo(directoryPath);

            List<string> targetDirectoriesName = folderDir.GetDirectories()
                                                            .Select(d => d.FullName)
                                                            .OrderBy(s=>s.Length)
                                                            .ThenBy(s=>s)
                                                            .ToList<string>();
         
            using (Bitmap bitmap = new Bitmap(xLength, yLength, PixelFormat.Format24bppRgb))
            {
                using (Graphics graphic = Graphics.FromImage(bitmap))
                {
                    int x, y = 0;
                    foreach(string currentDirectoryName in targetDirectoriesName)
                    {
                        x = 0; 
                        workingAnimation = new MergePictureWorkingAnimation();
                        foreach (string pictureFullName in GetMapPieces(currentDirectoryName))
                        {
                           
                            workingAnimation.Spin(currentDirectoryName);

                            pictureResizer = new PictureResizer();
                            Image resizedImage = pictureResizer.Resize(Image.FromFile(pictureFullName), (xLength / 128), (yLength / 128));
                            
                            graphic.DrawImage(resizedImage
                                        , x == 0 ? 0 : x * (xLength/128)
                                        , y == 0 ? 0 : y * (yLength/128));
                             x++;
                        }
                        y++;
                    }
                    graphic.Save();
                }
                bitmap.Save(finalPath, ImageFormat.Jpeg);
            }
        }
        
        private IEnumerable<string> GetMapPieces(string currentDirectoryName)
        {
            DirectoryInfo directory = new DirectoryInfo(currentDirectoryName);

            return directory.GetFiles().Select(s => s.FullName).OrderBy(s => s.Length).ThenBy(s => s);
        }      
        
    }
}